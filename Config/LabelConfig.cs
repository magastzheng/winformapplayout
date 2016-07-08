using Model.config;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class LabelConfig
    {
        private const string FileName = "message.json";

        private Dictionary<string, List<Label>> _labels = new Dictionary<string, List<Label>>();
        public LabelConfig()
        {
            Init();
        }

        private int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _labels = JsonConvert.DeserializeObject<Dictionary<string, List<Label>>>(content);

            return 0;
        }

        public string GetErrorMessage(int id)
        {
            string text = string.Empty;
            if (_labels.ContainsKey("errors"))
            {
                foreach (var label in _labels["errors"])
                {
                    if (label.Id == id)
                    {
                        text = label.Message;
                        break;
                    }
                }
            }

            return text;
        }
    }
}
