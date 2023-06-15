using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    [Table("VehicleModel")]
    public class VehicleModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("VehicleMake")]
        public int MakeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abrv { get; set; }
        
        public VehicleMake Make { get; set; }
    }
}
