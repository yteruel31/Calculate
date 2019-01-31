using System.Collections.Generic;
using Calculate.Model;

namespace Calculate.WPF.Services
{
    public interface IFormulaDataService
    {
        void DeleteFormula();
        List<Formula> GetAllFormulas();
        void AddFormula(Formula formula);
    }
}