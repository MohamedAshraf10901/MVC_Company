using MVC_Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    // model converted to table in database
    public class Employee :BaseEntity
    {
        //public int Id { get; set; }

        //[Required(ErrorMessage ="Name is Required!!")]
        [Required]
        public string Name { get; set; }
        
        //[Range(22,45)]
        public int? Age { get; set; }
        public decimal Salary { get; set; }

        //[RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }   //123-Street-City-Country

        //[Phone]
        public string Phone { get; set; }

        //[EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        //[DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }


        public string ImageName { get; set; }


        //[DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }


        public int? DepartmentId { get; set; }   //FK
        public Department Department { get; set; }


    }
}
