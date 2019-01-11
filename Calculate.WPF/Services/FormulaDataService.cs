using System.Collections.Generic;
using Calculate.DAL;
using Calculate.Model;

namespace Calculate.WPF.Services
{
    public class FormulaDataService : IFormulaDataService
    {
        private IFormulaRepository repository;

        public FormulaDataService(IFormulaRepository repository)
        {
            this.repository = repository;
        }

        public List<Formula> GetAllFormulas()
        {
            return repository.GetFormulas();
        }

        public void AddFormula(Formula formula)
        {
            repository.AddFormula(formula);
        }
    }
}