using Model.UI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class ImportConfig
    {
        private const string FileName = "import.json";

        private List<ImportSheet> _importSheet;
        public List<ImportSheet> ImportSheet { get { return _importSheet; } }

        public ImportConfig()
        {
            Init();
        }

        private void Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _importSheet = JsonConvert.DeserializeObject<List<ImportSheet>>(content);
        }

        public ImportSheet GetSheet(string id)
        {
            ImportSheet targetSheet = new ImportSheet();
            foreach (ImportSheet sheet in _importSheet)
            {
                if (id == sheet.Id)
                {
                    targetSheet = sheet;
                    break;
                }
            }

            return targetSheet;
        }
    }
}
