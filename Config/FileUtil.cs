using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class FileUtil
    {
        public static string ReadFile(string filePath)
        {
            string content = string.Empty;
            try
            {
                content = File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch(Exception e)
            {
                Console.WriteLine("Fail to read file: {0}, {1}", filePath, e.Message);
            }
            finally
            { 
            
            }

            return content;
        }
    }
}
