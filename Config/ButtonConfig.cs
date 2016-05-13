using Controls.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            foreach(var bg in _buttons)
            {
                if (bg.Id == name)
                {
                    buttonGroup = bg;
                    break;
                }
            }

            return buttonGroup;
        }
    }
}
