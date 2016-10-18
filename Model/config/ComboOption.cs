using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.config
{
    public class ComboOptionItem
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Order1 { get; set; }
        public int Order2 { get; set; }
        public string Name { get; set; }
        public object Data { get; set; }

        public string Text 
        {
            get 
            {
                string ret = string.Empty;
                if (!string.IsNullOrEmpty(Code)&&!string.IsNullOrEmpty(Name))
                {
                    if (Name.StartsWith("-"))
                    {
                        ret = string.Format("{0}{1}", Code, Name);
                    }
                    else
                    {
                        ret = string.Format("{0} {1}", Code, Name);
                    }
                }
                else if (!string.IsNullOrEmpty(Name))
                {
                    ret = Name;
                }
                else
                { 
                    //Do nothing
                }

                return ret;
            }
        }

        //public override string ToString()
        //{
        //    string ret = string.Empty;
        //    if (!string.IsNullOrEmpty(Code) || !string.IsNullOrEmpty(Name))
        //    {
        //        ret = string.Format("{0}{1}", Code, Name);
        //    }

        //    return ret;
        //}
    }

    public class ComboOption
    {
        public string Name { get; set; }
        public string Selected { get; set; }
        public List<ComboOptionItem> Items { get; set; }
    }
}
