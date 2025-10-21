/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-11
 * Time: 12:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;

using System.Linq;
using System.CodeDom;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Microsan
{
    /// <summary>
    /// Description of RuntimeProgramming.
    /// </summary>
    public class RuntimeProgramming
    {
        public const string RES_NAME_ROOT_CLASS_TEMPLATE = ".RootClass_Template.cs"; // embedded resource
        public const string RES_NAME_NEW_CLASS_TEMPLATE = ".NewClass_Template.cs"; // embedded resource

        public const string SOURCE_FILES_DIR_NAME = "RuntimeSourceFiles";
        public const string SOURCE_TEMP_FILES_DIR_NAME = "RuntimeTempSourceFiles";
        public const string COMPILE_TEMP_OUT_DIR_NAME = "RuntimeCompiles";
        public const string RootNameSpace = "RuntimeProgrammingNamespace";
        public const string RootClassName = "RootClass";
        public const string RootMainMethodName = "RootMain";
        public const string RootStartOnceMethodName = "StartOnce";
        
        public List<SourceFile> sourceFiles;
        
        public static string RuntimeCompileOutputFolder = System.IO.Directory.GetCurrentDirectory() + "\\" + COMPILE_TEMP_OUT_DIR_NAME + "\\";
        public static string RuntimeCompileTempSourceFilesOutputFolder = System.IO.Directory.GetCurrentDirectory() + "\\" + SOURCE_TEMP_FILES_DIR_NAME + "\\";

        public static string currDir = System.IO.Directory.GetCurrentDirectory() + "\\";
        public int RuntimeCompileCurrentIndex = 0;

        public SourceCodeEditControl srcEditCtrl;
        public Form srcEditContainerForm = null;


        public CSharpCodeProvider csSharpCodeProvider;
        public CompilerParameters compilerParams;
        
        public object RootObject = null;

        public Assembly CompiledAssembly;
        public Action<object> MainMethodDelegate;

        public bool CompileError = false;
        public bool ScriptChanged = true;

        private bool virtualFiles = false;
        public Action SaveAll = null;

        public Action CompiledAndRunning = null;

        public static string GetEmbeddedResourceName_EndsWith(string value)
        {
            Assembly a = Assembly.GetExecutingAssembly();

            string[] ar = a.GetManifestResourceNames();
            for (int i = 0; i < ar.Length; i++)
                if (ar[i].EndsWith(value))
                    return ar[i];
            return "";
        }
        public void ListEmbeddedResources()
        {
            Assembly a = Assembly.GetExecutingAssembly();
          
            string[] ar = a.GetManifestResourceNames();

            srcEditCtrl.AppendLineToLog("embedded resources:");
            for (int i = 0; i < ar.Length; i++)
                srcEditCtrl.AppendLineToLog("  " + ar[i]);
        }

        public RuntimeProgramming(object rootObject)
        {
            if (rootObject == null)
                this.RootObject = this;
            else
                this.RootObject = rootObject;

            Init_CSSharpRuntimeCompiler();
        }

        
        
        public static string GetEmbeddedTemplateResource(string _name)
        {
            string name = "";
            //MessageBox.Show("name:" + name);
            Assembly a = Assembly.GetExecutingAssembly();
            if (_name.StartsWith("."))
                name = GetEmbeddedResourceName_EndsWith(_name);
            // ✅ Check if the resource exists first
            if (name == "")
            {
                MessageBox.Show("Resource not found: " + _name);
                return "";
            }

            using (Stream s = a.GetManifestResourceStream(name))
            using (StreamReader sr = new StreamReader(s))
            {
                return sr.ReadToEnd();
            }
        }

        private void CreateNewRootSourceFile()
        {
            SourceFile sf = new SourceFile(currDir + SOURCE_FILES_DIR_NAME + "\\" + RootClassName + ".cs");
            sf.Contents = GetEmbeddedTemplateResource(RES_NAME_ROOT_CLASS_TEMPLATE);
            if (sf.Contents.Length != 0)
                sf.SaveFile();
        }
        
        private void Init_CSSharpRuntimeCompiler()
        {
            EmptyRuntimeCompileOutputFolder();

            csSharpCodeProvider = new CSharpCodeProvider();
            compilerParams = new CompilerParameters();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    string location = assembly.Location;
                    if (!String.IsNullOrEmpty(location))
                    {
                        compilerParams.ReferencedAssemblies.Add(location);
                    }
                }
                catch (NotSupportedException)
                {
                    // this happens for dynamic assemblies, so just ignore it. 
                }
            }
            
            // Reference to System.Drawing library
            //if (!compilerParams.ReferencedAssemblies.Contains("System.Drawing"))
            //    compilerParams.ReferencedAssemblies.Add("System.Drawing.dll");
            bool alreadyAdded = compilerParams.ReferencedAssemblies
                .Cast<string>()
                .Any(r => Path.GetFileNameWithoutExtension(r)
                    .Equals("System.Data", StringComparison.OrdinalIgnoreCase));

            if (!alreadyAdded)
                compilerParams.ReferencedAssemblies.Add("System.Data.dll");
            // True - memory generation, false - external file generation
            compilerParams.GenerateInMemory = false;
            // True - exe file generation, false - dll file generation
            compilerParams.GenerateExecutable = false;
            compilerParams.IncludeDebugInformation = true;
            //compilerParams.CompilerOptions = " /doc:" + currentSourceFile + ".xml";
            // compilerParams.IncludeDebugInformation = true;
        }
        
        private void EmptyRuntimeCompileOutputFolder()
        {
            if (System.IO.Directory.Exists(RuntimeCompileOutputFolder))
            {
                string[] files = System.IO.Directory.GetFiles(RuntimeCompileOutputFolder);
                for (int i = 0 ; i < files.Length ; i++)
                {
                    System.IO.File.Delete(files[i]);
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(RuntimeCompileOutputFolder);
            }
        }
        /// <summary>
        /// Creates new instance of SourceCodeEditControl if not done before.
        /// <para>And initialize it.</para>
        /// </summary>
        public void InitScriptEditor_IfNeeded()
        {
            if (srcEditCtrl != null) return;
        	srcEditCtrl = new SourceCodeEditControl(FastColoredTextBoxNS.Language.CSharp);
            srcEditCtrl.Save = srcEditCtrl_Save;
            srcEditCtrl.Execute = srcEditCtrl_ExecuteCode;
            srcEditCtrl.SaveAll = srcEditCtrl_SaveAll;

            if (srcEditCtrl.Parent == null) Init_SrcEditCtrl_ContainerForm();

        }
        public void ShowScriptEditor()
        {
            virtualFiles = false;
            if (srcEditCtrl.Parent == null) Init_SrcEditCtrl_ContainerForm();
            if (srcEditContainerForm != null) srcEditContainerForm.Visible = true;
            LoadSourceFilesFromDisc();
            srcEditCtrl.Show(sourceFiles, RootClassName + ".cs");
        }

        public void ShowScriptEditor(List<SourceFile> sourceFiles)
        {
            
            if (srcEditCtrl.Parent == null) Init_SrcEditCtrl_ContainerForm();
            if (srcEditContainerForm != null) srcEditContainerForm.Visible = true;
            SetSourceFiles(sourceFiles);
            srcEditCtrl.Show(sourceFiles, RootClassName + ".cs", true);
        }

        /// <summary>
        /// This is only used when using virtual files,
        /// note this method sets virtualFiles = true
        /// </summary>
        /// <param name="sourceFiles"></param>
        public void SetSourceFiles(List<SourceFile> sourceFiles)
        {
            virtualFiles = true;
            bool rootFileNeedsToBeCreated = true;
            string rootFileName = RootClassName + ".cs";
            for (int i = 0; i < sourceFiles.Count; i++)
            {
                if (sourceFiles[i].FileName == rootFileName)
                {
                    rootFileNeedsToBeCreated = false;
                    break;
                }
            }
            if (rootFileNeedsToBeCreated)
            {
                SourceFile sf = new SourceFile();
                sf.FileName = rootFileName;
                sf.Contents = GetEmbeddedTemplateResource(RES_NAME_ROOT_CLASS_TEMPLATE);
                sourceFiles.Insert(0, sf);
            }
            this.sourceFiles = sourceFiles;
        }

        private void LoadSourceFilesFromDisc()
        {
            if (!System.IO.Directory.Exists(currDir + SOURCE_FILES_DIR_NAME))
                System.IO.Directory.CreateDirectory(currDir + SOURCE_FILES_DIR_NAME);

            if (!System.IO.File.Exists(currDir + SOURCE_FILES_DIR_NAME + "\\" + RootClassName + ".cs"))
                CreateNewRootSourceFile();
            else if (new System.IO.FileInfo(currDir + SOURCE_FILES_DIR_NAME + "\\" + RootClassName + ".cs").Length == 0)
                CreateNewRootSourceFile();

            sourceFiles = new List<SourceFile>();
            string[] files = System.IO.Directory.GetFiles(currDir + SOURCE_FILES_DIR_NAME, "*.cs");
            for (int i = 0; i < files.Length; i++)
            {
                sourceFiles.Add(new SourceFile(files[i], true));
            }
        }

        private void Init_SrcEditCtrl_ContainerForm()
        {
            //srcEditCtrl.AppendLineToLog("--Init_SrcEditCtrl_ContainerForm");
            srcEditContainerForm = new Form();
            srcEditContainerForm.Text = "Microsan84 - RuntimeProgramming Editor";
            double newFormHeight = (double)Screen.GetWorkingArea(srcEditContainerForm.Location).Height * 0.8f;
            srcEditContainerForm.Size = new System.Drawing.Size(600, Convert.ToInt32(newFormHeight));
            srcEditContainerForm.FormClosing +=
                delegate (object s, FormClosingEventArgs fcea)
                {
                    if (fcea.CloseReason == CloseReason.UserClosing) fcea.Cancel = true;
                    srcEditContainerForm.Visible = false;
                    srcEditCtrl_SaveAll();
                };
            srcEditContainerForm.Controls.Add(srcEditCtrl);
            srcEditCtrl.Dock = DockStyle.Fill;
        }
        
        private void srcEditCtrl_Save(string fileName)
        {
            CompileError = false;
            ScriptChanged = true;
            fileName = fileName.ToLower();
            for (int i = 0; i < sourceFiles.Count; i++)
            {
                if (sourceFiles[i].FileName.ToLower() == fileName)
                {
                    sourceFiles[i].SaveFile();
                    break;
                }
            }
        }
        private void srcEditCtrl_SaveAll()
        {
            CompileError = false;
            ScriptChanged = true;
            if (virtualFiles == false)
            {
                for (int i = 0; i < sourceFiles.Count; i++)
                {
                    sourceFiles[i].SaveFile();
                }
            }
            else
            {
                SaveAll?.Invoke();
            }
            
        }

        public void ExecuteDefaultCode()
        {
            CompileError = false;
            if (CompileAndGetMainMethodDelegate())
                TryStart_MainMethod();
        }

        private void srcEditCtrl_ExecuteCode()
        {
            ExecuteDefaultCode();
            CompiledAndRunning?.Invoke();
        }
        public bool CompileAndGetMainMethodDelegate()
        {
            if (CompileError) return false;
            if (!CompileCode())
            {
                CompileError = true;
                return false;
            }

            MethodInfo mainMethod;

            // ✅ Try explicit name first (for backwards compatibility)
            GetMethodInfoRes res = GetMethodInfo(RootNameSpace, RootClassName, RootMainMethodName, out mainMethod);
            if (res != GetMethodInfoRes.Success)
            {
                // 🔍 Fallback: try to auto-detect "Main" or [EntryPoint]
                mainMethod = FindAutoEntryMethod();
                if (mainMethod == null)
                {
                    PrintGetMethodInfoResErrors(res, RootNameSpace, RootClassName, RootMainMethodName);
                    return false;
                }
                    
            }

            try
            {
                MainMethodDelegate = mainMethod.GetDelegate<object>();
                return true;
            }
            catch (Exception ex)
            {
                srcEditCtrl.AddToDgvLog("", -1, -1, "Delegate creation failed: " + ex.Message);
                return false;
            }
        }


        private MethodInfo FindAutoEntryMethod()
        {
            foreach (var type in CompiledAssembly.GetTypes())
            {
                // 1️⃣ Look for [EntryPoint] first
                var method = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .FirstOrDefault(m => m.GetCustomAttributes().Any(a => a.GetType().Name == "EntryPointAttribute"));
                if (method != null)
                    return method;

                // 2️⃣ Fallback to static Main() or Run()
                method = type.GetMethod("Main", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                      ?? type.GetMethod("Run", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (method != null)
                    return method;
            }
            return null;
        }

        public bool CompileCode()
        {
            ScriptChanged = false;
            InitScriptEditor_IfNeeded();
            srcEditCtrl.ClearLog();

            if (compilerParams.GenerateExecutable)
                compilerParams.OutputAssembly = RuntimeCompileOutputFolder + "RC_" + RuntimeCompileCurrentIndex++ + ".exe";
            else
                compilerParams.OutputAssembly = RuntimeCompileOutputFolder + "RC_" + RuntimeCompileCurrentIndex++ + ".dll";
            CompilerResults results = null;

            string[] sourceFilePaths = new string[sourceFiles.Count];

            if (virtualFiles == false)
            {
                for (int i = 0; i < sourceFiles.Count; i++)
                {
                    sourceFilePaths[i] = sourceFiles[i].FullFilePath;
                }
            }
            else
            {
                // first remove old files
                if (Directory.Exists(RuntimeCompileTempSourceFilesOutputFolder)) {
                    try { Directory.Delete(RuntimeCompileTempSourceFilesOutputFolder, true); } catch { /* ignore */ }
                }
                Directory.CreateDirectory(RuntimeCompileTempSourceFilesOutputFolder);

                for (int i = 0; i < sourceFiles.Count; i++)
                {
                    sourceFilePaths[i] = Path.Combine(RuntimeCompileTempSourceFilesOutputFolder, sourceFiles[i].FileName);
                    File.WriteAllText(sourceFilePaths[i], sourceFiles[i].Contents);
                }
            }
            results = csSharpCodeProvider.CompileAssemblyFromFile(compilerParams, sourceFilePaths);

            if (results.Errors.HasErrors)
            {
                foreach (CompilerError error in results.Errors)
                {
                    srcEditCtrl.AddToDgvLog(System.IO.Path.GetFileName(error.FileName), error.Line, error.Column, "Error (" + error.ErrorNumber + "): " + error.ErrorText);
                }
                return false;
            }
            CompiledAssembly = results.CompiledAssembly;
            return true;
        }

        public enum GetMethodInfoRes
        {
            Success,
            CompiledAssemblyNull,
            TypeNotFound,
            MethodNotFound            
        }
        public GetMethodInfoRes GetMethodInfo(string namespaceName, string className, string methodName, out MethodInfo methodInfo)
        {
            if (CompiledAssembly == null || CompileError)
            {
                methodInfo = null;
                return GetMethodInfoRes.CompiledAssemblyNull;
            }
            Type binaryFunction = CompiledAssembly.GetType(namespaceName + "." + className);
            if (binaryFunction == null)
            {
                //srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + namespaceName + "." + className + " is not found");
                methodInfo = null;
                return GetMethodInfoRes.TypeNotFound;
            }

            methodInfo = binaryFunction.GetMethod(methodName);
            if (methodInfo == null)
            {
                //srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + methodName + " is not found");
                return GetMethodInfoRes.MethodNotFound;
            }
            return GetMethodInfoRes.Success;
        }

        private void PrintGetMethodInfoResErrors(GetMethodInfoRes res, string namespaceName, string className, string methodName)
        {
            if (res == GetMethodInfoRes.CompiledAssemblyNull)
                srcEditCtrl.AddToDgvLog("", -1, -1, "error: CompiledAssemblyNull");
            else if (res == GetMethodInfoRes.TypeNotFound)
                srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + namespaceName + "." + className + "/Main-method/[EntryPoint] is not found");
            else if (res == GetMethodInfoRes.MethodNotFound)
                srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + methodName + "/Main-method/[EntryPoint] is not found");
        }

        public bool GetMethodDelegate(string namespaceName, string className, string methodName, out Action action)
        {
            MethodInfo methodInfo;
            action = null;
            GetMethodInfoRes res = GetMethodInfo(namespaceName, className, methodName, out methodInfo);
            if (res != GetMethodInfoRes.Success)
            {
                PrintGetMethodInfoResErrors(res, namespaceName, className, methodName);
                return false;
            }
                

            try
            {
                action = methodInfo.GetDelegate();
                return true;
            }
            catch (Exception ex)
            {
                srcEditCtrl.AddToDgvLog("", -1, -1, "Exception: " + ex);
                return false;
            }
        }

        public bool GetMethodDelegate<T>(string namespaceName, string className, string methodName, out Action<T> action)
        {
            MethodInfo methodInfo;
            action = null;
            GetMethodInfoRes res = GetMethodInfo(namespaceName, className, methodName, out methodInfo);
            if (res != GetMethodInfoRes.Success)
            {
                PrintGetMethodInfoResErrors(res, namespaceName, className, methodName);
                return false;
            }
            try
            {
                action = methodInfo.GetDelegate<T>();
                return true;
            }
            catch (Exception ex)
            {
                srcEditCtrl.AddToDgvLog("", -1, -1, "Exception: " + ex);
                return false;
            }
        }

        public static string ReplaceIgnoreCase(string source, string oldValue, string newValue)
        {
            return Regex.Replace(source,
                Regex.Escape(oldValue),
                newValue.Replace("$", "$$"),
                RegexOptions.IgnoreCase);
        }

        public void TryStart_RootClass_Method(string name)
        {
            try
            {
                Action action;
                if (GetMethodDelegate(RuntimeProgramming.RootNameSpace, RuntimeProgramming.RootClassName, name, out action))
                    action();
            }
            catch (Exception ex)
            {
                string exStr = ex.ToString();
                if (virtualFiles) exStr = ReplaceIgnoreCase(exStr, RuntimeCompileTempSourceFilesOutputFolder, "");
                srcEditCtrl.AddToDgvLog("", -1, -1, "Run Exception: \n" + exStr);
            }
        }
        public void TryStart_RootClass_Method<T>(string name, T parameter)
        {
            try
            {
                Action<T> action;
                if (GetMethodDelegate<T>(RuntimeProgramming.RootNameSpace, RuntimeProgramming.RootClassName, name, out action))
                    action(parameter);
            }
            catch (Exception ex)
            {
                string exStr = ex.ToString();
                if (virtualFiles) exStr = ReplaceIgnoreCase(exStr, RuntimeCompileTempSourceFilesOutputFolder, "");
                srcEditCtrl.AddToDgvLog("", -1, -1, "Run Exception: \n" + exStr);
            }
        }

        public void TryStart_MainMethod()
        {
            if (MainMethodDelegate == null) return;
            try
            { 
                MainMethodDelegate(RootObject);
            }
            catch (Exception ex)
            {
                string exStr = ex.ToString();
                
                if (virtualFiles) exStr = ReplaceIgnoreCase(exStr, RuntimeCompileTempSourceFilesOutputFolder, "");
                srcEditCtrl.AddToDgvLog("", -1, -1, "Run Exception: \n" + exStr);
            }
        }

        public List<MethodExecShortcutEntry> GenerateShortcutsFromCompiledAssembly()
        {
            var entries = new List<MethodExecShortcutEntry>();
            if (CompiledAssembly == null || CompileError)
            {
                return entries;
            }
            foreach (var type in CompiledAssembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    var attr = method.GetCustomAttribute<ShortcutAttribute>();
                    if (attr != null)
                    {
                        entries.Add(new MethodExecShortcutEntry
                        {
                            Namespace = type.Namespace ?? "",
                            Class = type.Name,
                            Method = method.Name,
                            DisplayName = attr.DisplayName,
                            IconName = attr.IconName,
                            Execute = method.GetDelegate()
                        });
                    }
                }
            }

            return entries;
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ShortcutAttribute : Attribute
    {
        public string DisplayName { get; }
        public string IconName { get; }  // optional

        public ShortcutAttribute(string displayName, string iconName = null)
        {
            DisplayName = displayName;
            IconName = iconName;
        }
    }
    public class MethodExecShortcutEntry
    {
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public string DisplayName { get; set; }

        public string IconName { get; set; }

        // Optional cached delegate
        public Action Execute { get; set; }
    }

    public static class ReflectionExt
    {
        public static Action<T> CreateDelegate<T>(MethodInfo methodInfo)
        {
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), methodInfo);
        }
        public static Action CreateDelegate(MethodInfo methodInfo)
        {
            return (Action)Delegate.CreateDelegate(typeof(Action), methodInfo);
        }
        public static Action<T> GetDelegate<T>(this MethodInfo methodInfo)
        {
            return CreateDelegate<T>(methodInfo);
        }
        public static Action GetDelegate(this MethodInfo methodInfo)
        {
            return CreateDelegate(methodInfo);
        }
    }
    public class SourceFile
    {
        public string FileName { get; set; } = "";
        public string FileDirPath { get; set; } = "";
        public string Contents { get; set; } = "";
        public int editorSelectionStart { get; set; } = 0;
        public int editorSelectionLength { get; set; } = 0;
        public int editorVerticalScrollValue { get; set; } = 0;
        public int editorHorizontalScrollValue { get; set; } = 0;
        
        public string FileNameWithoutExt
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(FileName); }
        }
        
        public string FullFilePath 
        {
            get {
                if (FileDirPath == "")
                    return FileName;
                else
                    return FileDirPath + "\\" + FileName;
            }
            set{
                FileName = System.IO.Path.GetFileName(value);
                FileDirPath = System.IO.Path.GetDirectoryName(value);
            }
        }

        public SourceFile()
        {

        }
        
        /// <summary>
        /// Creates new empty source file, it also creates an empty file on the disk 
        /// </summary>
        /// <param name="filePath"></param>
        public SourceFile(string filePath)
        {
            FullFilePath = filePath;
            System.IO.File.WriteAllText(filePath, "");
        }
        
        public SourceFile(string filePath, bool readFile)
        {
            FullFilePath = filePath;
            if (readFile)
                ReadFile();
        }
        
        public void ReadFile()
        {
            if (System.IO.File.Exists(FullFilePath))
            {
                Contents = System.IO.File.ReadAllText(FullFilePath);
            }
        }
        
        public void SaveFile()
        {
            System.IO.File.WriteAllText(FullFilePath, Contents);
        }

        public void SBAppendAsJson(StringBuilder sb, string lineincr, string keyName, object data, bool last)
        {
            sb.Append(lineincr);
            sb.Append("  "); 
            sb.Append($"  \"{keyName}\": "); 
            sb.Append(JsonConvert.SerializeObject(data));
            if (!last) sb.AppendLine(",");
            else sb.AppendLine();
        }

        public string ToJsonString(string lineincr)
        {
            var sb = new StringBuilder();
            sb.Append("  "); sb.Append(lineincr); sb.AppendLine("{");

            SBAppendAsJson(sb, lineincr, "fileName", FileName, false);
            SBAppendAsJson(sb, lineincr, "contents", Contents, false);
            SBAppendAsJson(sb, lineincr, "editorSelectionStart", editorSelectionStart, false);
            SBAppendAsJson(sb, lineincr, "editorSelectionLength", editorSelectionLength, false);
            SBAppendAsJson(sb, lineincr, "editorVerticalScrollValue", editorVerticalScrollValue, false);
            SBAppendAsJson(sb, lineincr, "editorHorizontalScrollValue", editorHorizontalScrollValue, true);

            sb.Append("  "); sb.Append(lineincr); sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
