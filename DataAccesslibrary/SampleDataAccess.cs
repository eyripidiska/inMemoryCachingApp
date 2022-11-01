using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<EmployeeModel> GetEmployee()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { Firstname = "Tim", LastName = "Corey" });
            output.Add(new() { Firstname = "Sue", LastName = "Storm"});
            output.Add(new() { Firstname = "Jane", LastName = "Jones"});

            Thread.Sleep(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeeAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { Firstname = "Tim", LastName = "Corey" });
            output.Add(new() { Firstname = "Sue", LastName = "Storm" });
            output.Add(new() { Firstname = "Jane", LastName = "Jones" });

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeeCached()
        {
            List<EmployeeModel> output;

            output = _memoryCache.Get<List<EmployeeModel>>("employees");

            if (output is null)
            {
                output = new();

                output.Add(new() { Firstname = "Tim", LastName = "Corey" });
                output.Add(new() { Firstname = "Sue", LastName = "Storm" });
                output.Add(new() { Firstname = "Jane", LastName = "Jones" });

                await Task.Delay(3000);

                _memoryCache.Set("employees", output, TimeSpan.FromMinutes(1));
            }

            return output;
        }
    }
}
