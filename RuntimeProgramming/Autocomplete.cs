using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
    public class Autocomplete
    {
        AutocompleteMenu popupMenu;
        static string SearchPattern = @"[\w\.:=!<>]";
        string[] keywords = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()"};
        string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do${\n^;\n}while();", "switch(^)\n{\ncase : break;\n}"};
        string[] declarationSnippets = { 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };

        FastColoredTextBox fctb;
        List<AutocompleteItem> items;

        public Action<string> Debug;

        public Autocomplete(FastColoredTextBox fctb)
        {
            this.fctb = fctb;
            //create autocomplete popup menu
            popupMenu = new AutocompleteMenu(fctb);
            popupMenu.MinimumSize = new Size(512, 256);
           // popupMenu
            //popupMenu.Items.ImageList = imageList1;
            popupMenu.SearchPattern = SearchPattern;
            popupMenu.AllowTabKey = true;
            //
            BuildAutocompleteMenu();
            fctb.KeyPressed += fctb_KeyPressed;
        }

        private void PrintDebug(string text)
        {
            if (Debug != null) Debug(text);
        }

        private void BuildAutocompleteMenu()
        {
            items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
        }
        void fctb_KeyPressed(object sender, KeyPressEventArgs e)
        {
            bool backspaceORdel = e.KeyChar == '\b' || e.KeyChar == 0xff || e.KeyChar == (char)Keys.Return;
            if (backspaceORdel) return;

            Range fragment = fctb.Selection.GetFragment(SearchPattern);
            string text = fragment.Text.Trim();
            if (text.Length == 0) return;

            Type t = Type.GetType(text);
            if (t == null)
                t = System.Reflection.Assembly.GetExecutingAssembly().GetType(text);
            if (t == null) { /*PrintDebug("not a type");*/ return; }
            //PrintDebug("is a type");
            string[] typeMethods = MyRefactor.GetMethods(t);

            if (typeMethods.Length == 0) { PrintDebug("have no methods"); return; }
            //PrintDebug("Type:" + text);
            RemoveAllItemsByType(typeof(MethodAutocompleteItem));
            foreach (var item in typeMethods)
            {
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
                //PrintDebug(item);
            }
            popupMenu.Items.SetAutocompleteItems(items);
        }
        public void RemoveAllItemsByType(Type autocompleteItemType)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].GetType() == autocompleteItemType.GetType())
                    items.RemoveAt(i);
            }
        }

        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern):base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if(Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null) 
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }
    }
    public static class MyRefactor
    {
        public static string[] GetMethods(Type t)
        {
            System.Reflection.MethodInfo[] mi = t.GetMethods();
            string[] MethodNames = new string[mi.Length];
            for (int i = 0; i< mi.Length; i++)
            {
                MethodNames[i] = mi[i].Name + mi[i].GetParameters().ToHumanString();
            }
            return MethodNames;
        }
        public static string ToHumanString(this System.Reflection.ParameterInfo[] pi)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder("(");

            for (int i = 0; i < pi.Length; i++)
            {
                sb.Append(pi[i].ParameterType.Name);//.ToString());
                sb.Append(" ");
                sb.Append(pi[i].Name);
                if (i < (pi.Length - 1)) sb.Append(", ");
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
   /* Type binaryFunction = CompiledAssembly.GetType(namespaceName + "." + className);
            if (binaryFunction == null)
            {
                srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + namespaceName + "." + className + " is not found");
                methodInfo = null;
                return false;
            }

methodInfo = binaryFunction.GetMethod(methodName);
            if (methodInfo == null)
            {
                srcEditCtrl.AddToDgvLog("", -1, -1, "error: " + methodName + " is not found");
                return false;
            }
            return true;*/
}
