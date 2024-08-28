using MVC_Demo.BLL.Interfaces;
using MVC_Demo.DAL.Contexts;
using MVC_Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BLL.Repositories;

namespace MVC_Demo.BLL.Repositories
{
    public class DepartmentRepository : GenaricRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            
        }





        //private readonly AppDbContext _context /*= new AppDbContext()*/;
        //public DepartmentRepository(AppDbContext context)  // Ask Clr create object from AppDbContext  -  injection
        //{
        //    //_context = new AppDbContext();
        //    _context = context;

        //}

        //public IEnumerable<Department> GetAll()
        //{

        //    return _context.Departments.ToList();
        //}



        //public Department Get(int id)
        //{
        //   return _context.Departments.FirstOrDefault(d => d.Id == id); 
        //}



        //public int Add(Department entity)
        //{ 
        //    if(entity is not null)
        //    {
        //        _context.Add(entity);
        //        return _context.SaveChanges();
        //    }
        //    return -1;
        //}


        //public int Update(Department entity)
        //{
        //    _context.Update(entity);    
        //    return (_context.SaveChanges());
        //}



        //public int Delete(Department model)
        //{
        //    _context.Departments.Remove(model);
        //    return _context.SaveChanges();
        //}

        /*public int Delete(int entity)
        {
            _context.Departments.Remove(_context.Departments.Where(d => d.Id == entity).FirstOrDefault());           
            return _context.SaveChanges();  
        }*/


    }
}
