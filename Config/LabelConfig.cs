using Model.config;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class LabelConfig
    {
        private const string FileName = "message.json";

        private Message _message = new Message();
        public LabelConfig()
        {
            Init();
        }

        private int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _message = JsonConvert.DeserializeObject<Message>(content);

            return 0;
        }

        public string GetErrorMessage(int id)
        {
            string text = string.Empty;
            if (_message != null && _message.Errors != null)
            {
                var label = _message.Errors.Find(p => p.Id == id);
                if (label != null)
                {
                    text = label.Message;
                }
            }

            return text;
        }

        public string GetLabelText(string id)
        {
            string text = string.Empty;
            if (_message != null && _message.Labels != null)
            {
                var label = _message.Labels.Find(p => p.Id.Equals(id));
                if (label != null)
                {
                    text = label.Text;
                }
            }

            return text;
        }
    }
}
