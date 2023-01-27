using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC.Models
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
