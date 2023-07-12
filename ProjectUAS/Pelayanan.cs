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
    public partial class Pelayanan : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;

        public Pelayanan()
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
            string str = "select No_Transaksi, Tgl_pengiriman, Berat_Pengiriman, Id_Pengepul, Id_Kurir from dbo.Rekap_Pelayanan";
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
            string noTransaksi = nop.Text;
            string tglpengiriman = dtptanggal1.Text;
            string brtPengiriman = brt.Text;
            string idPengepul = idp1.Text;
            string idKurir = idp2.Text;
            if (noTransaksi == "")
            {
                MessageBox.Show("Masukkan No Penanaman", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (tglpengiriman == "")
            {
                MessageBox.Show("Masukkan Tanggal Tanam", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                koneksi.Open();
                string str = "Insert Into Rekap_Pelayanan ( No_Transaksi, Tgl_pengiriman, Berat_Pengiriman, Id_Pengepul, Id_Kurir) values (@nop, @tgl1, @brt, @idp1, @idp2)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@nop", noTransaksi));
                cmd.Parameters.Add(new SqlParameter("@tgl1", tglpengiriman));
                cmd.Parameters.Add(new SqlParameter("@brt", brtPengiriman));
                cmd.Parameters.Add(new SqlParameter("@idp1", idPengepul));
                cmd.Parameters.Add(new SqlParameter("@idp2", idKurir));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string noTransaksi = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Pelayanan where No_Transaksi = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noTransaksi);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Nomor Transaksi tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Nomor Transaksi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Rekap_Pelayanan where No_Transaksi = @nop";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@nop", noTransaksi);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Nomor Transaksi berhasil dihapus.");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string noTransaksi = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select No_Transaksi, Tgl_pengiriman, Berat_Pengiriman, Id_Pengepul, Id_Kurir From Rekap_Pelayanan Where No_Transaksi = @nop ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@nop", noTransaksi);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Nomor Transaksi tidak ditemukan.");

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
            string noTransaksi = nop.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Pelayanan where No_Transaksi = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noTransaksi);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Data Pelayanan tidak ditemukan.");
                        return;
                    }
                }
                string noTransaksi2 = nop.Text;
                string tglpengiriman = dtptanggal1.Text;
                string brtPengiriman = brt.Text;
                string idPengepul = idp1.Text;
                string idKurir = idp2.Text;
                string str2 = "Update Rekap_Pelayanan set No_Transaksi = @nop, Tgl_pengiriman= @tgl1, " +
                    "Berat_Pengiriman = @brt, Id_Pengepul = @idp1, Id_Kurir = @idp2 where No_Transaksi= @nop";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@nop", noTransaksi2));
                cmd2.Parameters.Add(new SqlParameter("@tgl1", tglpengiriman));
                cmd2.Parameters.Add(new SqlParameter("@brt", brtPengiriman));
                cmd2.Parameters.Add(new SqlParameter("@idp1", idPengepul));
                cmd2.Parameters.Add(new SqlParameter("@idp2", idKurir));
                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Pelayanan berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate data Pelayanan.");
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
                nop.Text = row.Cells["No_Transaksi"].Value.ToString();
                dtptanggal1.Text = row.Cells["Tgl_pengiriman"].Value.ToString();
                brt.Text = row.Cells["Berat_Pengiriman"].Value.ToString();
                idp1.Text = row.Cells["Id_Pengepul"].Value.ToString();
                idp2.Text = row.Cells["Id_Kurir"].Value.ToString();
            }
        }
    }
}
