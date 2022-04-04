using Stock_Vivero.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ProductsRepository : BaseRepository<ProductViewModel>
    {

        //El primer string corresponde al nombre de la tabla
        //El segundo string corresponde al nombre que acompañaria a "id EJMPLO"
        public ProductsRepository() : base("Products", "")
        {

        }
        public IEnumerable<ProductViewModel> ProductsObject()
        {
            var consulta = "SELECT * FROM Products";
            return QueryObject<ProductViewModel>(consulta, null);
        }
    }
}
