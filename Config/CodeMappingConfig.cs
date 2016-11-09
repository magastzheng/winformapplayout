using Model.config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class CodeMappingConfig
    {
        private const string FileName = "codemapping.json";

        private CodeMapping _codeMapping = null;

        public CodeMappingConfig()
        {
            Init();
        }

        private int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _codeMapping = JsonConvert.DeserializeObject<CodeMapping>(content);

            return 0;
        }

        public CodeMappingItem GetItemBySecuCode(string secuCode)
        {
            return _codeMapping.Mapping.Find(p => p.SecuCode.Equals(secuCode)); 
        }

        public CodeMappingItem GetItemByWindCode(string windCode)
        {
            return _codeMapping.Mapping.Find(p => p.WindCode.Equals(windCode));
        }
    }
}
