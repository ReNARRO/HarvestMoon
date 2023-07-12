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
            btnDelete.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string noPenanaman = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Penanaman where No_Penanaman = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPenanaman);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Nomor Penanaman tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Nomor Penanaman?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Rekap_Penanaman where No_Penanaman = @nop";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@nop", noPenanaman);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Nomor Penanaman berhasil dihapus.");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string noPenanaman = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select No_Penanaman, Tgl_tanam, Tgl_Panen, Berat_Pangan, Id_Pangan, Id_Petani From Rekap_Penanaman Where No_Penanaman = @nop ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@nop", noPenanaman);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Nomor Penanaman tidak ditemukan.");

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
            string noPenanaman = nop.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Penanaman where No_Penanaman = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPenanaman);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Data Penanaman tidak ditemukan.");
                        return;
                    }
                }
                string noPenanaman2 = nop.Text;
                string tglTanam = dtptanggal1.Text;
                string tglPanen = dtptanggal2.Text;
                string brtPanen = brt.Text;
                string idPangan = idp1.Text;
                string idPetani = idp2.Text;
                string str2 = "Update Rekap_Penanaman set No_Penanaman = @nop, Tgl_tanam= @tgl1, Tgl_Panen = @tgl2, " +
                    "Berat_Pangan = @brt, Id_Pangan = @idp1, Id_Petani = @idp2 where No_Penanaman = @nop";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@nop", noPenanaman2));
                cmd2.Parameters.Add(new SqlParameter("@tgl1", tglTanam));
                cmd2.Parameters.Add(new SqlParameter("@tgl2", tglPanen));
                cmd2.Parameters.Add(new SqlParameter("@brt", brtPanen));
                cmd2.Parameters.Add(new SqlParameter("@idp1", idPangan));
                cmd2.Parameters.Add(new SqlParameter("@idp2", idPetani));
                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Penanaman berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate data Penanaman.");
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                nop.Text = row.Cells["No_Penanaman"].Value.ToString();
                dtptanggal1.Text = row.Cells["Tgl_tanam"].Value.ToString();
                dtptanggal2.Text = row.Cells["Tgl_Panen"].Value.ToString();
                brt.Text = row.Cells["Berat_Pangan"].Value.ToString();
                idp1.Text = row.Cells["Id_Pangan"].Value.ToString();
                idp2.Text = row.Cells["Id_Petani"].Value.ToString();
            }
        }
    }
}
