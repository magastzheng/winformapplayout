using Model.config;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            var target = _buySellOption.Find(p => p.Name.Equals(name));
            if (target != null)
            {
                comboOption = target;
            }

            return comboOption;
        }
    }
}
