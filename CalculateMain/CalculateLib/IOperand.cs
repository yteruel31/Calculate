﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    interface IOperand
    {
        Calculate[] MakeLogical(LogicTree[] logicalTree);
    }
}
