using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Test
{
    public class TestLogRepository: ILogRepository
    {
        public List<Log> ListAll()
        {
            return new List<Log>()
            {
                new Log {ExceptionDate = DateTime.Parse("1/1/2015"), ErrorMessage = "" },
                new Log {ExceptionDate = DateTime.Parse("2/1/2015"), ErrorMessage = "NullReferneceException" }
            };
        }

        public void Add(Log logEntry)
        {
            return;
        }
    }
}