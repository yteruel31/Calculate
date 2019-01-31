using Calculate.DAL;
using Calculate.Model;
using System.Collections.Generic;

namespace Calculate.WPF.Services
{
    public class FormulaDataService : IFormulaDataService
    {
        private readonly IFormulaRepository _repository;

        public FormulaDataService(IFormulaRepository repository)
        {
            _repository = repository;
        }

        public void AddFormula(Formula formula)
        {
            _repository.AddFormula(formula);
        }

        public void DeleteFormula()
        {
            _repository.DeleteFormula();
        }

        public List<Formula> GetAllFormulas()
        {
            return _repository.GetFormulas();
        }
    }
}