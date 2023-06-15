using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.ViewModels
{
    public class VehicleModelVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select vehicle make.")]
        public int MakeId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Abrv { get; set; }
        
        public VehicleMake? Make { get; set; }
    }
}
