using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.BLL
{
    public class TaxManager
    {
        private IStateTaxInformationRespository _myStateTaxInformationRespository;

        public TaxManager(IStateTaxInformationRespository aStateTaxInformationRespository)
        {
            _myStateTaxInformationRespository = aStateTaxInformationRespository;
        }

        public List<StateTaxInformation> ListAll()
        {
            return _myStateTaxInformationRespository.ListAll();
        }

        public decimal GetRate(string stateAbbreviation)
        {
            var allStates = _myStateTaxInformationRespository.ListAll();

            var state = allStates.First(s => s.StateAbbreviation == stateAbbreviation);

            return state.TaxRate;
        }

        public bool IsValidState(string stateAbbreviation)
        {
            var allStates = _myStateTaxInformationRespository.ListAll();

            return allStates.Any(s => s.StateAbbreviation == stateAbbreviation);
        }
    }
}
