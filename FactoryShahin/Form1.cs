using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShahinDataBaseConfigue;
using ShahinShoese;
using BaseForms;
namespace FactoryShahin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ShahinFactoryDb db = new ShahinFactoryDb();
                db.Auction.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void تعریفسایزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(),typeof(Size),"",null,true,"تعریف سایز");
            F.ShowDialog();
        }

        private void تعریفانبارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(), typeof(Store), "", null, true, "تعریف انبار");
            F.ShowDialog();
        }

        private void تعریفواحدمحصولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(), typeof(ProductUnit), "", null, true, "تعریف واحد محصول");
            F.ShowDialog();
        }

        private void تعریفنوعمحصولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(), typeof(ProductType), "", null, true, "تعریف نوع محصول");
            F.ShowDialog();
        }

        private void تعریفنوعمشترToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(), typeof(CustomerType), "", null, true, "تعریف نوع مشتری");
            F.ShowDialog();
        }

        private void تعریفمشتریToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBase F = new FormBase(new ShahinFactoryDb(), typeof(Customer), "", null, true, "تعریف مشتری");
            F.ShowDialog();
        }
    }
}
