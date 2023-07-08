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
    public partial class Penanaman : Form
    {
        public Penanaman()
        {
            InitializeComponent();
        }

        private void btnPenanaman_Click(object sender, EventArgs e)
        {
            Penanaman pe = new Penanaman();
            pe.Show();
            this.Hide();
        }

        private void btnPenyuplaian_Click(object sender, EventArgs e)
        {
            Penyuplaian py = new Penyuplaian();
            py.Show();
            this.Hide();
        }

        private void btnPelayanan_Click(object sender, EventArgs e)
        {
            Pelayanan pl = new Pelayanan();
            pl.Show();
            this.Hide();
        }

        private void btnPengiriman_Click(object sender, EventArgs e)
        {
            Pengiriman pr = new Pengiriman();
            pr.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DataRelasi dr = new DataRelasi();
            dr.Show();
            this.Hide();
        }
    }
}
