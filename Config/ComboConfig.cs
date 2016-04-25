using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class ComboConfig
    {
        private const string FileName = "uiconfig.json";

        private List<ComboOption> _buySellOption;
        public List<ComboOption> BuySellOption { get { return _buySellOption; } }

        public ComboConfig()
        {
            Init();
        }

        public int Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _buySellOption = JsonConvert.DeserializeObject<List<ComboOption>>(content);

            return 0;
        }

        public ComboOption GetComboOption(string name)
        {
            ComboOption comboOption = new ComboOption();

            foreach(var option in _buySellOption)
            {
                if (option.Name == name)
                {
                    comboOption = option;
                    break;
                }
            }

            return comboOption;
        }
    }
}
