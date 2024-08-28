using Demo.DAL.Models;
using MVC_Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {

        Task<IEnumerable<Employee>> GetByName(string name);



        //Employee GetByAddress(string address);


        //public IEnumerable<Employee> GetAll();
        //public Employee Get(int id);
        //public int Add(Employee entity);
        //public int Update(Employee entity);        
        //public int Delete(Employee model);
    }
}
