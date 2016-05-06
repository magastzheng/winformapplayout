using Controls.Entity;
using Model.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class NavbarConfig
    {
        private const string FileName = "navbar.json";

        private List<TSNavNodeData> _barDataList = new List<TSNavNodeData>();
        public List<TSNavNodeData> BarDataList { get { return _barDataList; } }
        public NavbarConfig()
        {
            Init();
        }

        private void Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _barDataList = JsonConvert.DeserializeObject<List<TSNavNodeData>>(content);
        }
    }
}
