using ClassLibrary1;
using ClassLibrary1.Models;
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
    public partial class FormStock : Form
    {
        ProductsRepository productsRepository = new ProductsRepository();
        IEnumerable<ProductViewModel> listProducts;
        public FormStock()
        {
            InitializeComponent();
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            listProducts = productsRepository.ProductsObject();
            dataGridViewStock.DataSource = listProducts;
        }

        private void Refesh(string text = null)
        {
            if (text != "")
            {
                int j = listProducts.Count();
                int a = 0;
                string textAux = "";
                List<ProductViewModel> asd = new List<ProductViewModel>();
                foreach (ProductViewModel lstProduct in listProducts)
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        textAux += lstProduct.Name[i].ToString();
                    }
                    if (text == textAux)
                    {
                        asd.Add(listProducts.ElementAt(a));
                    }
                    a++;
                    textAux = "";
                }
                a = 0;
                dataGridViewStock.DataSource = asd;
            }
            else dataGridViewStock.DataSource = listProducts;

            if (text == null) dataGridViewStock.DataSource = listProducts;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Refesh(textBox1.Text);
        }

        private void dataGridViewStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = dataGridViewStock.CurrentCell.RowIndex;
            a = listProducts[a].Id;
            productsRepository.Eliminar<ProductViewModel>(a);
        }
    }
}
