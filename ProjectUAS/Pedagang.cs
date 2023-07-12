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
    public partial class Pedagang : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Pedagang()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringconnection);
            refreshform();
        }
        private void refreshform()
        {
            nmp.Text = "";
            nmp.Enabled = false;
            idp.Text = "";
            idp.Enabled = false;
            alp.Text = "";
            alp.Enabled = false;
            nop.Text = "";
            nop.Enabled = false;
            stok.Text = "";
            stok.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;

        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select Id_Pedagang, Nama_Pedagang, Alamat, No_HP, Stok_Penjualan from dbo.Pedagang";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
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
            refreshform();
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            idp.Enabled = true;
            nmp.Enabled = true;
            alp.Enabled = true;
            nop.Enabled = true;
            stok.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idPedagang = idp.Text;
            string nmPedagang = nmp.Text;
            string alPedagang = alp.Text;
            string noPedagang = nop.Text;
            string stokPenjualan = stok.Text;
            if (idPedagang == "")
            {
                MessageBox.Show("Masukkan Id Pedagang", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (nmPedagang == "")
            {
                MessageBox.Show("Masukkan Nama Pedagang", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            else
            {
                koneksi.Open();
                string str = "Insert Into Pedagang (Id_Pedagang, Nama_Pedagang, Alamat, No_HP, Stok_Penjualan) values (@idp, @nmp, @alp, @nop, @stk)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@idp", idPedagang));
                cmd.Parameters.Add(new SqlParameter("@nmp", nmPedagang));
                cmd.Parameters.Add(new SqlParameter("@alp", alPedagang));
                cmd.Parameters.Add(new SqlParameter("@nop", noPedagang));
                cmd.Parameters.Add(new SqlParameter("@stk", stokPenjualan));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idPedagang = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Pedagang where Id_Pedagang = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idPedagang);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Pedagang tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Id Pedagang?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Pedagang where Id_Pedagang = @idp";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@idp", idPedagang);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Id Petani berhasil dihapus.");
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
            string idpedagang = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Id_Pedagang, Nama_Pedagang, Alamat, No_HP, Stok_Penjualan From Pedagang Where Id_Pedagang = @idp ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@idp", idpedagang);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Id Pedagang tidak ditemukan.");

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
            string idPedagang= idp.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Pedagang where Id_Pedagang = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idPedagang);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Pedagang tidak ditemukan.");
                        return;
                    }
                }
                string idPedagang2 = idp.Text;
                string nmPedagang = nmp.Text;
                string alPedagang = alp.Text;
                string noPedagang = nop.Text;
                string stokPenjualan = stok.Text;
                string str2 = "Update Pedagang set Id_Pedagang = @idp, Nama_Pedagang= @nmp, Alamat = @alp, No_HP = @nop, Stok_Penjualan = @stk where Id_Pedagang = @idp";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@idp", idPedagang2));
                cmd2.Parameters.Add(new SqlParameter("@nmp", nmPedagang));
                cmd2.Parameters.Add(new SqlParameter("@alp", alPedagang));
                cmd2.Parameters.Add(new SqlParameter("@nop", noPedagang));
                cmd2.Parameters.Add(new SqlParameter("@stk", stokPenjualan));

                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Pedagang berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate Data Pedagang.");
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
                idp.Text = row.Cells["Id_Pedagang"].Value.ToString();
                nmp.Text = row.Cells["Nama_Pedagang"].Value.ToString();
                alp.Text = row.Cells["Alamat"].Value.ToString();
                nop.Text = row.Cells["No_HP"].Value.ToString();
                stok.Text = row.Cells["Stok_Penjualan"].Value.ToString();
            }
        }
    }
}
