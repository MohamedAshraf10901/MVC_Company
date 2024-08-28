using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.DAL.Contexts;
using MVC_Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) :base(context)
        {
            
        }

        public async Task<IEnumerable<Employee>> GetByName(string name)
        {
            return await _context.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.Department).ToListAsync();
        }



        //private readonly AppDbContext _context /*= new AppDbContext()*/;
        //public EmployeeRepository(AppDbContext context)  // Ask Clr create object from AppDbContext  -  injection
        //{
        //    //_context = new AppDbContext();
        //    _context = context;

        //}

        //public IEnumerable<Employee> GetAll()
        //{

        //    return _context.Employees.ToList();
        //}



        //public Employee Get(int id)
        //{
        //    return _context.Employees.FirstOrDefault(d => d.Id == id);
        //}



        //public int Add(Employee entity)
        //{
        //    if (entity is not null)
        //    {
        //        _context.Add(entity);
        //        return _context.SaveChanges();
        //    }
        //    return -1;
        //}


        //public int Update(Employee entity)
        //{
        //    _context.Update(entity);
        //    return (_context.SaveChanges());
        //}



        //public int Delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}

    }
}
