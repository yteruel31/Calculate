using System.Collections.Generic;
using Calculate.DAL;
using Calculate.Model;

namespace Calculate.WPF.Services
{
    public class FormulaDataService : IFormulaDataService
    {
        private readonly IFormulaRepository _repository;

        public FormulaDataService(IFormulaRepository repository)
        {
            _repository = repository;
        }

        public List<Formula> GetAllFormulas()
        public void DeleteFormula()
        {
            _repository.DeleteFormula();
        }

        public void AddFormula(Formula formula)
        {
            _repository.AddFormula(formula);
        }
    }
}