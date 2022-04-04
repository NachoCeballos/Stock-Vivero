using ClassLibrary1;
using Stock_Vivero.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Vivero
{
    public partial class FormAddStock : Form
    {
        ProductsRepository productsRepository = new ProductsRepository();
        public FormAddStock()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            keyValuePairs.Add("Type", txtType.Text);
            keyValuePairs.Add("Name", txtName.Text);
            keyValuePairs.Add("Price", txtPrice.Text);
            keyValuePairs.Add("Stock", txtStock.Text);
            productsRepository.Agregar<ProductViewModel>(keyValuePairs);
            Clear();
        }

        private void Clear()
        {
            txtType.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
        }
    }
}
