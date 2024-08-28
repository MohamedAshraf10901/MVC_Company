using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Demo.DAL.Models
{
    //Model Class - Poco Class
    //ViewModel
    public class Department :BaseEntity
    {
        //public int Id { get; set; }  //pk 1,1

        [Required(ErrorMessage ="Code IS Required")]
        public string Code { get; set; } //optional
        
        [Required(ErrorMessage = "Name IS Required")]
        public string Name { get; set; }
        
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }



        public ICollection<Employee> Employees { get; set; }

    }
}
