using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class StateTaxRepository : IStateTaxInformationRespository
    {
        private const string FilePath = @"DataFiles\Taxes.txt";

        public List<StateTaxInformation> ListAll()
        {
            if (!File.Exists(FilePath))
                throw new Exception("Tax file was not found!");

            var taxes = new List<StateTaxInformation>();
            var reader = File.ReadAllLines(FilePath);

            for (int i = 1; i < reader.Length; i++)
            {
                var columns = reader[i].Split(',');
                var tax = new StateTaxInformation();

                tax.StateAbbreviation = columns[0];
                tax.TaxRate = decimal.Parse(columns[1]);

                taxes.Add(tax);
            }

            return taxes;
        }
    }
}