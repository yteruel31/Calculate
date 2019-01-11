﻿using System.Collections.Generic;
using Calculate.Model;

namespace Calculate.DAL
{
    public interface IFormulaRepository
    {
        void AddFormula(Formula formula);
        List<Formula> GetFormulas();
    }
}