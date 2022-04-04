using ClassLibrary1;
//using ClassLibrary1.Models.ViewModels;
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
    public partial class Form1 : Form
    {
        private Form currentChildForm = new Form();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock=DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();

            childForm.Show();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormStock());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
            currentChildForm.Close();
        }

        private void Reset()
        {
            
        }

        private void btnLoadStock_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAddStock());
        }
    }
}
