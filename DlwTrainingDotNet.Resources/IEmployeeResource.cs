using System.Collections.Generic;
using System.Threading.Tasks;
using DlwTrainingDotNet.Resources.Entities;

namespace DlwTrainingDotNet.Resources
{
    public interface IEmployeeResource
    {
        Task<IEnumerable<EmployeeEntity>> GetAll();
        Task Add(EmployeeEntity employee);
        void Add();
        Task Delete(int id);
    }
}
