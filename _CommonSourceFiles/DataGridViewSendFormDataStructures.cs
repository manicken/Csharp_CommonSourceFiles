/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-05
 * Time: 23:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Crom.Controls.TabbedDocument;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;

namespace Microsan
{
    /// <summary>
    /// 
    /// </summary>
    public class SendDataJsonItems
    {
        /// <summary>
        /// 
        /// </summary>
        public BindingList<SendDataItem> items = new BindingList<SendDataItem>();
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToJsonString(string lineincr)
        {
            string jsonStr = lineincr + "  {";
            try
            {
                jsonStr += "\"name\":" + JsonConvert.SerializeObject(Name) + ",";
                jsonStr += "\"items\":[\n";
                int count = items.Count;
                for (int i = 0; i < count; i++)
                {
                    jsonStr += lineincr + "    " + items[i].ToJsonString();
                    if (i < count - 1)
                        jsonStr += ",\n";

                }
                jsonStr += lineincr + "\n    ]}";
                return jsonStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get JSON string: {ex.Message}");
                return "";
            }
        }
    }
    /** */
    public class SendDataJsonFile
    {
        /** */
        public BindingList<SendDataItem> data = new BindingList<SendDataItem>();
        private readonly string filePath;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }
        /** */
        public SendDataJsonFile(string filePath)
        {
            this.filePath = filePath;

            // Load file if it exists
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    data = JsonConvert.DeserializeObject<BindingList<SendDataItem>>(json) ?? new BindingList<SendDataItem>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load JSON file: {ex.Message}");
                    data = new BindingList<SendDataItem>();
                }
            }
            else // else create new with empty contents
            {
                File.WriteAllText(filePath, "[]");
            }
        }
        /** */
        public void Save()
        {
            try
            {
                string FileNameNoExt = Path.GetFileNameWithoutExtension(filePath);
                string fileDirectory = Path.GetDirectoryName(filePath);
                string test = "[\n";
                int count = data.Count;
                for (int i = 0; i < count; i++)
                {
                    test += "  " + data[i].ToJsonString();
                    if (i < count - 1)
                        test += ",\n";

                }
                test += "\n]\n";
                File.WriteAllText(fileDirectory + "\\" + FileNameNoExt + ".json", test);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save JSON file: {ex.Message}");
            }
        }
    }
    /**
     * 
     */
    public class SendDataItem
    {
        /** */
        public string Data { get; set; }
        /** */
        public string Note { get; set; }

        /** */
        public string ToJsonString()
        {
            // Use JsonSerializer to escape strings properly
            return $"{{\"data\": {JsonConvert.SerializeObject(Data)}, \"note\": {JsonConvert.SerializeObject(Note)}}}";
        }
    }
}