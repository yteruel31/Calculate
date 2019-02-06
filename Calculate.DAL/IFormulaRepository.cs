using Calculate.Model;
using System.Collections.Generic;

namespace Calculate.DAL
{
    public interface IFormulaRepository
    {
        void AddFormula(Formula formula);

        void DeleteFormula();

        List<Formula> GetFormulas();
    }
}