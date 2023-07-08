using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectUAS
{
    public partial class HalamanUtama : Form
    {
        public HalamanUtama()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataMaster dm = new DataMaster();
            dm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRelasi dr = new DataRelasi();
            dr.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
