using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class ConfigManager
    {
        private readonly static ConfigManager _instance = new ConfigManager();

        private ComboConfig _comboConfig;
        private FunctionConfig _functionConfig;
        private SystemConfig _systemConfig;
        private TerminalConfig _terminalConfig;
        private GridConfig _gridConfig;
        private LabelConfig _labelConfig;
        private NavbarConfig _navBarConfig;

        private ConfigManager()
        {
            Init();
        }

        static ConfigManager()
        { 
        
        }

        private int Init()
        {
            _comboConfig = new ComboConfig();
            _functionConfig = new FunctionConfig();
            _systemConfig = new SystemConfig();
            _terminalConfig = new TerminalConfig(_systemConfig);
            _gridConfig = new GridConfig();
            _labelConfig = new LabelConfig();
            _navBarConfig = new NavbarConfig();

            //_buySellConfig.Init();
            //_functionConfig.Init();

            return 0;
        }

        public static ConfigManager Instance { get { return _instance; } }

        public ComboConfig GetComboConfig()
        {
            return _instance._comboConfig;
        }

        public FunctionConfig GetFunctionConfig()
        {
            return _instance._functionConfig;
        }

        public SystemConfig GetSystemConfig()
        {
            return _instance._systemConfig;
        }

        public TerminalConfig GetTerminalConfig()
        {
            return _instance._terminalConfig;
        }

        public GridConfig GetGridConfig()
        {
            return _instance._gridConfig;
        }

        public LabelConfig GetLabelConfig()
        {
            return _instance._labelConfig;
        }

        public NavbarConfig GetNavbarConfig()
        {
            return _instance._navBarConfig;
        }
    }
}
