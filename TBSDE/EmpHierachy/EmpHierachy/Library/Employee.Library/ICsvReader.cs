using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Library
{
    public interface ICsvReader
    {
        Task<List<Employyee>> GetEmployees(string csvFilePath);
    }
}
