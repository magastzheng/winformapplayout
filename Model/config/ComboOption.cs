﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ComboOptionItem
    {
        public string Id { get; set; }
        public string Order { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0}  {1}", Order, Name);
        }
    }

    public class ComboOption
    {
        public string Name { get; set; }
        public string Selected { get; set; }
        public List<ComboOptionItem> Items { get; set; }
    }
}
