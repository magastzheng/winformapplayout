using Controls.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Config
{
    public class GridConfig
    {
        private const string FileName = "gridview.json";

        //private static GridConfig _instance = new GridConfig();
        private List<HSGrid> _grids;
        public List<HSGrid> Grids { get { return _grids; } }
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
            _grids = JsonConvert.DeserializeObject<List<HSGrid>>(content);
        }

        public HSGrid GetGid(string name)
        {
            HSGrid targetGrid = new HSGrid();
            var target = _grids.Find(p => p.Grid.Equals(name));
            if (target != null)
            {
                targetGrid = target;
            }
            
            return targetGrid;
        }
    }
}
