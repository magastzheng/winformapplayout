using Model.Data;
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
            var target = _importSheet.Find(p => p.Id.Equals(id));
            if (target != null)
            {
                targetSheet = target;
            }

            return targetSheet;
        }

        public List<DataColumnHeader> GetColumnHeader(List<DataHeader> cellRanges)
        {
            List<DataColumnHeader> columns = new List<DataColumnHeader>();

            foreach (var range in cellRanges)
            {
                columns.AddRange(range.Children);
            }

            return columns;
        }
    }
}
