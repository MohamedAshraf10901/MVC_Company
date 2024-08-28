using MVC_Demo.BLL.Interfaces;
using MVC_Demo.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using MVC_Demo.DAL.Models;
using Demo.BLL.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Demo.PL.Controllers
{
	//[AllowAnonymous]
	[Authorize]
	public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork)  // Ask CLR Create Object from IDepartmentRepository
        {
            //this._departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        // /Department/Index
        public async Task<IActionResult> Index()
        {

            //var dept = _departmentRepository.GetAll();
            var dept = await _unitOfWork.DepartmentRepository.GetAll();

            return View(dept);
        }


        // /Department/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)  // Server Side Validation
            {
                //int Count= _departmentRepository.Add(model);
                //int Count = _unitOfWork.DepartmentRepository.Add(model);
                _unitOfWork.DepartmentRepository.Add(model);
                var Count = await _unitOfWork.Complete();


                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }


        public async Task<IActionResult> Details(int? id, string ViewName = "Details") 
        {
            if (id is null)
                return BadRequest();   //400 ERROR

            //var department = _departmentRepository.Get(id.Value);
            var department = await _unitOfWork.DepartmentRepository.Get(id.Value);


            if (department is null)
                return NotFound();  //404 

            return View(ViewName, department);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        /*public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();   //400 ERROR

            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();  //404 
             
            return View(department);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id,Department model)
        {
            if(id !=model.Id)
                return BadRequest();  //400

            if (ModelState.IsValid)   // Server Side Validation
            {
                //var count = _departmentRepository.Update(model);
                //var count = _unitOfWork.DepartmentRepository.Update(model);
                _unitOfWork.DepartmentRepository.Update(model);

                int count = await _unitOfWork.Complete();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        /* public IActionResult Delete(int? id)
         {
             if (id is null)
                 return BadRequest();   //400 ERROR

             var department = _departmentRepository.Get(id.Value);

             if (department is null)
                 return NotFound();  //404 

             return View(department);
         }*/

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department model)
        {
            if (id != model.Id)
                return BadRequest();  //400

            if (ModelState.IsValid)   // Server Side Validation
            {
                //var count = _departmentRepository.Delete(model);
                //var count = _unitOfWork.DepartmentRepository.Delete(model);
                _unitOfWork.DepartmentRepository.Delete(model);
                int count = await _unitOfWork.Complete();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
    }
}
