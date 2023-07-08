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
    public partial class Petani : Form
    {
        public Petani()
        {
            InitializeComponent();
        }

        private void btnPetani_Click(object sender, EventArgs e)
        {
            Petani pe = new Petani();
            pe.Show();
            this.Hide();
        }

        private void btnPengepul_Click(object sender, EventArgs e)
        {
            Pengepul pg = new Pengepul();
            pg.Show();
            this.Hide();
        }

        private void btnKurir_Click(object sender, EventArgs e)
        {
            Kurir ku = new Kurir();
            ku.Show();
            this.Hide();
        }

        private void btnPedagang_Click(object sender, EventArgs e)
        {
            Pedagang pd = new Pedagang();
            pd.Show();
            this.Hide();
        }

        private void btnPangan_Click(object sender, EventArgs e)
        {
            Pangan pa = new Pangan();
            pa.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DataMaster dm = new DataMaster();
            dm.Show();
            this.Hide();
        }
    }
}
