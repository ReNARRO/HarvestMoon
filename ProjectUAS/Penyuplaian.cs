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
    public partial class Penyuplaian : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Penyuplaian()
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
            brt.Text = "";
            brt.Enabled = false;
            dtptanggal2.Enabled = false;
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
            string str = "select No_Penyuplaian, Tgl_Menyuplai, Berat_Suplai, Tgl_Menerima,  Id_Petani, Id_Pengepul  from dbo.Rekap_Penyuplaian";
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
            string noPenyuplaian = nop.Text;
            string tglMenyuplai = dtptanggal1.Text;
            string brtSuplai = brt.Text;
            string tglMenerima = dtptanggal2.Text;            
            string idPetani = idp1.Text;
            string idPengepul = idp2.Text;
            if (noPenyuplaian == "")
            {
                MessageBox.Show("Masukkan No Penyuplaian", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (tglMenyuplai == "")
            {
                MessageBox.Show("Masukkan Tanggal Menyuplai", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                koneksi.Open();
                string str = "Insert Into Rekap_Penyuplaian (No_Penyuplaian, Tgl_Menyuplai, Berat_Suplai, Tgl_Menerima,  Id_Petani, Id_Pengepul) values (@nop, @tgl1, @brt, @tgl2, @idp1, @idp2)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@nop", noPenyuplaian));
                cmd.Parameters.Add(new SqlParameter("@tgl1", tglMenyuplai));
                cmd.Parameters.Add(new SqlParameter("@brt", brtSuplai));
                cmd.Parameters.Add(new SqlParameter("@tgl2", tglMenerima));               
                cmd.Parameters.Add(new SqlParameter("@idp1", idPetani));
                cmd.Parameters.Add(new SqlParameter("@idp2", idPengepul));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
