using Controls.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class ButtonConfig
    {
        private const string FileName = "buttons.json";

        private List<ButtonGroup> _buttons = null;

        public List<ButtonGroup> Buttons { get { return _buttons; } }

        public ButtonConfig()
        {
            Init();
        }

        public int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _buttons = JsonConvert.DeserializeObject<List<ButtonGroup>>(content);

            return 0;
        }

        public ButtonGroup GetButtonGroup(string name)
        {
            ButtonGroup buttonGroup = new ButtonGroup();
            var target = _buttons.Find(p => p.Id.Equals(name));
            if (target != null)
            {
                buttonGroup = target;
            }

            return buttonGroup;
        }
    }
}
