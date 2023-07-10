using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Data.SqlClient;

namespace ProjectUAS
{
    public partial class Penanaman : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Penanaman()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringconnection);
            refreshform();
        }
        private void refreshform()
        {
            nop.Text = "";
            nop.Enabled = false;
            dtptanggal1.Enabled = false;
            dtptanggal2.Enabled = false;
            brt.Text = "";
            brt.Enabled = false;
            idp1.Text = "";
            idp1.Enabled = false;
            idp2.Text = "";
            idp2.Enabled = false;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select No_Penanaman, Tgl_tanam, Tgl_Panen, Berat_Pangan, Id_Pangan, Id_Petani from dbo.Rekap_Penanaman";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
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

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {           
            nop.Enabled = true;
            dtptanggal1.Enabled = true;
            dtptanggal2.Enabled = true;
            brt.Enabled = true;
            idp1.Enabled = true;
            idp2.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string noPenanaman = nop.Text;
            string tglTanam = dtptanggal1.Text;
            string tglPanen = dtptanggal2.Text;
            string brtPanen = brt.Text;
            string idPangan = idp1.Text;
            string idPetani = idp2.Text;
            if (noPenanaman == "")
            {
                MessageBox.Show("Masukkan No Penanaman", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (tglTanam == "")
            {
                MessageBox.Show("Masukkan Tanggal Tanam", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            else
            {
                koneksi.Open();
                string str = "Insert Into Rekap_Penanaman ( No_Penanaman, Tgl_tanam, Tgl_Panen, Berat_Pangan, Id_Pangan, Id_Petani) values (@nop, @tgl1, @tgl2, @brt, @idp1, @idp2)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@nop", noPenanaman));
                cmd.Parameters.Add(new SqlParameter("@tgl1", tglTanam));
                cmd.Parameters.Add(new SqlParameter("@tgl2", tglPanen));
                cmd.Parameters.Add(new SqlParameter("@brt", brtPanen));
                cmd.Parameters.Add(new SqlParameter("@idp1", idPangan));
                cmd.Parameters.Add(new SqlParameter("@idp2", idPetani));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }
    }
}
