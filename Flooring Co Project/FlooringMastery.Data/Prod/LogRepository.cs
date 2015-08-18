using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data.Prod
{
    public class LogRepository : ILogRepository
    {

        private const string FilePath = @"DataFiles\Logs.txt";

        public List<Log> ListAll()
        {
            if (File.Exists(FilePath))
            {
                var logs = new List<Log>();
                var reader = File.ReadAllLines(FilePath);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');

                    var log = new Log();

                    log.ExceptionDate = DateTime.Parse(columns[0]);
                    log.ErrorMessage = columns[1];

                    logs.Add(log);
                }
                return logs;
            }
            return null;
        }

        public void Add(Log logEntry)
        {
            var logs = ListAll();

            if (logs == null)
                logs = new List<Log>();

            logs.Add(logEntry);

            OverwriteFile(logs);
        }


        private void OverwriteFile(List<Log> logs)
        {

            if (File.Exists(FilePath))
                File.Delete(FilePath);

            using (var writer = File.CreateText(FilePath))
            {
                writer.WriteLine("ExceptionDate, ErrorMessage");

                foreach (var log in logs)
                {
                    writer.WriteLine("{0},{1}", log.ExceptionDate, log.ErrorMessage);
                }
            }
        }
    }
}
