using Controls.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class GridConfig
    {
        private const string FileName = "gridview.json";

        //private static GridConfig _instance = new GridConfig();
        private List<TSGrid> _grids;
        public List<TSGrid> Grids { get { return _grids; } }
        //public GridConfig Instance { get { return _instance; } }
        public GridConfig()
        {
            Init();
        }

        //static GridConfig()
        //{ 
        //}

        private void Init()
        {
            string filePath = RuntimeEnv.Instance.GetConfigFile(FileName);
            string content = FileUtil.ReadFile(filePath);
            _grids = JsonConvert.DeserializeObject<List<TSGrid>>(content);
        }

        public TSGrid GetGid(string name)
        {
            TSGrid targetGrid = new TSGrid();
            var target = _grids.Find(p => p.Grid.Equals(name));
            if (target != null)
            {
                targetGrid = target;
            }
            
            return targetGrid;
        }
    }
}
