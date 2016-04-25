﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public delegate void DataHandlerCallback(DataParser dataParser);

    public interface IDataHandler
    {
        void Handle(DataParser dataParser);
    }
}
