using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Model;
using System.Collections.Generic;
using Config;

namespace HSUnitTest
{
    [TestClass]
    public class JsonUnitTest
    {
        [TestMethod]
        public void TestJson()
        {
            string currentPath = Environment.CurrentDirectory;
            string path = @"D:\application\HundsunExtDemo\HundsunExtDemo\config\fieldtype.json";
            string content = string.Empty;
            try
            {
                if (!File.Exists(path))
                    return;
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                        reader.Close();
                    }
                    fs.Close();
                }
            }
            catch
            {
                Console.WriteLine("Fail to read file: {0}.", path);
            }

            try
            {
                var FieldTypes = JsonConvert.DeserializeObject<List<FieldConfigItem>>(content);
                Console.WriteLine(FieldTypes.Count);
            }
            catch
            {
                Console.WriteLine("Fail to deserialize FieldType {0}.", path);
            }
        }

        [TestMethod]
        public void TestFunctionConfig()
        {
            FunctionConfig conf = new FunctionConfig();
            conf.Init();

        }
    }
}
