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
            btnDelete.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;
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
            btnDelete.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = true;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string noPenyuplaian = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Penyuplaian where No_Penyuplaian = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPenyuplaian);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Nomor Penyuplaian tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Nomor Penyuplaian?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Rekap_Penyuplaian where No_Penyuplaian = @nop";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@nop", noPenyuplaian);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Nomor Penyuplaian berhasil dihapus.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
                dataGridView();
                refreshform();
            }
        }

        private void Penyuplaian_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string noPenyuplaian = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select No_Penyuplaian, Tgl_Menyuplai, Berat_Suplai, Tgl_Menerima,  Id_Petani, Id_Pengepul From Rekap_Penyuplaian Where No_Penyuplaian = @nop ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@nop", noPenyuplaian);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Nomor Penyuplaian tidak ditemukan.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string noPenyupalaian = nop.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Penyuplaian where No_Penyuplaian = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPenyupalaian);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Data Penyuplaian tidak ditemukan.");
                        return;
                    }
                }
                string noPenyuplaian = nop.Text;
                string tglMenyuplai = dtptanggal1.Text;
                string brtSuplai = brt.Text;
                string tglMenerima = dtptanggal2.Text;
                string idPetani = idp1.Text;
                string idPengepul = idp2.Text;
                string str2 = "Update Rekap_Penyuplaian set No_Penyuplaian = @nop, Tgl_Menyuplai = @tgl1,Berat_Suplai = @brt, Tgl_menerima = @tgl2, Id_Petani = @idp1, Id_Pengepul = @idp2 where No_Penyuplaian = @nop";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@nop", noPenyuplaian));
                cmd2.Parameters.Add(new SqlParameter("@tgl1", tglMenyuplai));
                cmd2.Parameters.Add(new SqlParameter("@brt", brtSuplai));
                cmd2.Parameters.Add(new SqlParameter("@tgl2", tglMenerima));
                cmd2.Parameters.Add(new SqlParameter("@idp1", idPetani));
                cmd2.Parameters.Add(new SqlParameter("@idp2", idPengepul));
                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Pengiriman berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate data Pengiriman.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
                dataGridView();
                refreshform();
            }
        }
    }
}
