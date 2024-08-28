using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using MVC_Demo.BLL.Interfaces;
using MVC_Demo.BLL.Repositories;
using MVC_Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IUnitOfWork /*, IDisposable*/
    {
        private readonly AppDbContext _context;
        private Lazy<IDepartmentRepository> departmentRepository;  //null
        private Lazy<IEmployeeRepository> employeeRepository;  //null

        public UnitOfWork(AppDbContext context)  //ask clr create from AppDbContext
        {
            _context = context;
            //departmentRepository = new DepartmentRepository(_context);
            //employeeRepository = new EmployeeRepository(_context);
            departmentRepository = new Lazy<IDepartmentRepository>(new DepartmentRepository(_context));
            employeeRepository = new Lazy<IEmployeeRepository>(new EmployeeRepository(_context));
        }

        public IDepartmentRepository DepartmentRepository => departmentRepository.Value;  /*{ get { return departmentRepository; } }*/
        public IEmployeeRepository EmployeeRepository => employeeRepository.Value;

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
