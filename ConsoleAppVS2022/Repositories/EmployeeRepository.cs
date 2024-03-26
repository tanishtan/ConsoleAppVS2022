using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.Repositories
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        EmployeeDbContext dbContext = new EmployeeDbContext();
        public Employee FindById(int id)
        {
            return dbContext.Employees.AsNoTracking().FirstOrDefault(c=>c.EmployeeId == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return dbContext.Employees.AsNoTracking().ToList();
        }

        public IEnumerable<Employee> GetByCriterial(string filterCriteria)
        {
            return null;
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void Upsert(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
