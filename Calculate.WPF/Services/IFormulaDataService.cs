using Calculate.Model;
using System.Collections.Generic;

namespace Calculate.WPF.Services
{
    public interface IFormulaDataService
    {
        void AddFormula(Formula formula);

        void DeleteFormula();

        List<Formula> GetAllFormulas();
    }
}