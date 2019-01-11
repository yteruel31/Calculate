using System.Collections.Generic;
using Calculate.Model;

namespace Calculate.DAL
{
    public class FormulaRepository : IFormulaRepository
    {
        private static List<Formula> _formulas;

        public FormulaRepository()
        {

        }
        public void AddFormula(Formula formula)
        {
            _formulas.Add(formula);
        }

        public List<Formula> GetFormulas()
        {
            if (_formulas == null)
            {
                LoadFormulas();
            }

            return _formulas;
        }

        private void LoadFormulas()
        {
            _formulas = new List<Formula>();
        }
    }
}