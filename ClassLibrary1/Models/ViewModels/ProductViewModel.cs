using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Vivero.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Precio")]
        public int? Price { get; set; }
        public int? Stock { get; set; }
    }
}
