using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DlwTrainingDotNet.Resources.Entities;
using Microsoft.EntityFrameworkCore;

namespace DlwTrainingDotNet.Resources
{
    public class EmployeeDatabaseResource : IEmployeeResource
    {
        private readonly DlwTrainingContext _context;

        public EmployeeDatabaseResource(DlwTrainingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAll()
        {
            return await _context.Employees.Take(10).ToListAsync();
        }

        public async Task Add(EmployeeEntity employee)
        {
            throw new Exception();
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            _context.Employees.Remove(new EmployeeEntity
            {
                ID = id
            });
            await _context.SaveChangesAsync();
        }
    }
}