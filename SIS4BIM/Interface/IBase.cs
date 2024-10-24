﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Interface
{
    public interface IBase<T>
    {
        int Insert(T t);
        int Delete(T t);
        int Update(T t);
        DataTable Select();
    }
}
