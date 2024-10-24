﻿using SIS4BIM.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS4BIM.Interface
{
    public interface IDetalle : IBase<Detalle>
    {
        Detalle Get(byte id);
    }
}
