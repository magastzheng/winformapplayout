
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
        private ImportConfig _importConfig;
        private ButtonConfig _buttonConfig;
        private TDFAPIConfig _tdfAPIConfig;
        private DefaultSettingConfig _defaultSettingConfig;
        private CodeMappingConfig _codeMappingConfig;

        private ConfigManager()
        {
            Init();
        }

        static ConfigManager()
        { 
        
        }

        private int Init()
        {
            //try
            //{
                _comboConfig = new ComboConfig();
                _functionConfig = new FunctionConfig();
                _systemConfig = new SystemConfig();
                _gridConfig = new GridConfig();
                _labelConfig = new LabelConfig();
                _navBarConfig = new NavbarConfig();
                _importConfig = new ImportConfig();
                _buttonConfig = new ButtonConfig();
                _tdfAPIConfig = new TDFAPIConfig();
                _defaultSettingConfig = new DefaultSettingConfig();
                _codeMappingConfig = new CodeMappingConfig();    

                _terminalConfig = new TerminalConfig(_systemConfig);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

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

        public ImportConfig GetImportConfig()
        {
            return _instance._importConfig;
        }

        public ButtonConfig GetButtonConfig()
        {
            return _instance._buttonConfig;
        }

        public TDFAPIConfig GetTDFAPIConfig()
        {
            return _instance._tdfAPIConfig;
        }

        public DefaultSettingConfig GetDefaultSettingConfig()
        {
            return _instance._defaultSettingConfig;
        }

        public CodeMappingConfig GetCodeMappingConfig()
        {
            return _instance._codeMappingConfig;
        }
    }
}
