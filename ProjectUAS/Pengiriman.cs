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
    public partial class Pengiriman : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Pengiriman()
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
            string str = "select No_Pengiriman, Tgl_Penerimaan, Id_Kurir, Id_Pedagang from dbo.Rekap_Pengiriman";
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

        private void Pengiriman_Load(object sender, EventArgs e)
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
            idp1.Enabled = true;
            idp2.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string noPengiriman = nop.Text;
            string tglPenerimaan = dtptanggal1.Text;
            string idKurir = idp1.Text;
            string idPedagang = idp2.Text;
            if (noPengiriman == "")
            {
                MessageBox.Show("Masukkan No Transaksi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (tglPenerimaan == "")
            {
                MessageBox.Show("Masukkan Tanggal Penerimaan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                koneksi.Open();
                string str = "Insert Into Rekap_Pengiriman ( No_Pengiriman, Tgl_Penerimaan, Id_Kurir, Id_Pedagang) values (@nop, @tgl1, @idp1, @idp2)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@nop", noPengiriman));
                cmd.Parameters.Add(new SqlParameter("@tgl1", tglPenerimaan));
                cmd.Parameters.Add(new SqlParameter("@idp1", idKurir));
                cmd.Parameters.Add(new SqlParameter("@idp2", idPedagang));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string noPengiriman = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Pengiriman where No_Pengiriman = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPengiriman);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Nomor Pengiriman tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Nomor Pengiriman?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Rekap_Pengiriman where No_Pengiriman = @nop";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@nop", noPengiriman);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Nomor Pengiriman berhasil dihapus.");
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
            string noPengiriman = nop.Text;
            try
            {
                koneksi.Open();
                string str = "Select No_Pengiriman, Tgl_Penerimaan, Id_Kurir, Id_Pedagang From Rekap_Pengiriman Where No_Pengiriman = @nop ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@nop", noPengiriman);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Nomor Pengiriman tidak ditemukan.");

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
            string noPengiriman = nop.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Rekap_Pengiriman where No_Pengiriman = @nop";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@nop", noPengiriman);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Dta Pengiriman tidak ditemukan.");
                        return;
                    }
                }
                string noPengiriman2 = nop.Text;
                string tglPenerimaan = dtptanggal1.Text;
                string idKurir = idp1.Text;
                string idPedagang = idp2.Text;
                string str2 = "Update Rekap_Pengiriman set No_Pengiriman = @nop, Tgl_Penerimaan = @tgl1, Id_Kurir = @idp1, Id_Pedagang = @idp2 where No_Pengiriman = @nop";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@nop", noPengiriman2));
                cmd2.Parameters.Add(new SqlParameter("@tgl1", tglPenerimaan));
                cmd2.Parameters.Add(new SqlParameter("@idp1", idKurir));
                cmd2.Parameters.Add(new SqlParameter("@idp2", idPedagang));
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                nop.Text = row.Cells["No_Pengiriman"].Value.ToString();
                dtptanggal1.Text = row.Cells["Tgl_Penerimaan"].Value.ToString();
                idp1.Text = row.Cells["Id_Kurir"].Value.ToString();
                idp2.Text = row.Cells["Id_Pedagang"].Value.ToString();
            }
        }
    }
}
