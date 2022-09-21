using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Library
{
    public class CsvReader : ICsvReader
    {
        public CsvReader()
        {

        }

        public async Task<List<Employyee>> GetEmployees(string csvFilePath)
        {
            return await Task.Run(() =>
            {
                //return File.ReadAllLines(csvFilePath).Skip(1).Where(s => s.Length > 1)
                //	.Select(l => l.ReadEmployeeFromCsvLine()).ToList();

                return File.ReadAllLines(csvFilePath).Where(s => s.Length > 1).Select(l => l.ReadEmployeeFromCsvLine()).ToList();
            });
        }
    }
}
