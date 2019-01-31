using Calculate.Model;
using System.Collections.Generic;

namespace Calculate.DAL
{
    public class FormulaRepository : IFormulaRepository
    {
        public void AddFormula(Formula formula)
        {
            if (Properties.Settings.Default.Formulas == null)
            {
                Properties.Settings.Default.Formulas = new FormulaCollection();
            }

            Properties.Settings.Default.Formulas.Add(formula);
            Properties.Settings.Default.Save();
        }

        public void DeleteFormula()
        {
            Properties.Settings.Default.Formulas.Clear();
            Properties.Settings.Default.Save();
        }

        public List<Formula> GetFormulas()
        {
            return Properties.Settings.Default.Formulas ?? new List<Formula>();
        }
    }
}