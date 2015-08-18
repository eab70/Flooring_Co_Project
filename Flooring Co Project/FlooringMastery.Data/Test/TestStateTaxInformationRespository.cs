using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Test
{
    public class TestStateTaxInformationRespository : IStateTaxInformationRespository
    {
        public List<StateTaxInformation> ListAll()
        {
            return new List<StateTaxInformation>()
            {
                new StateTaxInformation() { StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 0.0625M},
                new StateTaxInformation() { StateAbbreviation = "PA", StateName = "Pennsylvania", TaxRate = 0.075M},
                new StateTaxInformation() { StateAbbreviation = "NY", StateName = "New York", TaxRate = 0.1M},
            };
        }
    }
}
