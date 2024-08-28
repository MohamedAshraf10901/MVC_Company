using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenaricRepository<T> :IGenaricRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context /*= new AppDbContext()*/;
        public GenaricRepository(AppDbContext context)  // Ask Clr create object from AppDbContext  -  injection
        {
            //_context = new AppDbContext();
            _context = context;

        }


        public async Task<IEnumerable<T>> GetAll()
        {

            //return _context.Employees.ToList();
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Set<Employee>().Include(E => E.Department).ToListAsync();
            }
            else
            {
                return await _context.Set<T>().ToListAsync();
            }

        }


        //public IEnumerable<T> GetAll()
        //{

        //    //return _context.Employees.ToList();
        //    if (typeof(T) == typeof(Employee))
        //    {
        //        return (IEnumerable<T>)_context.Set<Employee>().Include(E=>E.Department).ToList();
        //    }
        //    else
        //    {
        //        return _context.Set<T>().ToList();
        //    }          

        //}



        public async Task<T> Get(int id)
        {
            //return _context.Employees.FirstOrDefault(d => d.Id == id);

            //return _context.Set<T>().Find(id);

            return await _context.Set<T>().FindAsync(id);


        }



        public void Add(T entity)
        {
            if (entity is not null)
            {
                _context.Add(entity);
                //return _context.SaveChanges();
            }
            //return -1;
        }

        //public async Task Add(T entity)
        //{
        //    if (entity is not null)
        //    {
        //        await _context.AddAsync(entity);
        //        //return _context.SaveChanges();
        //    }
        //    //return -1;
        //}


        public void Update(T entity)
        {
            _context.Update(entity);
            //return (_context.SaveChanges());
        }



        public void Delete(T model)
        {
            _context.Remove(model);
            //return _context.SaveChanges();
        }
    }
}
