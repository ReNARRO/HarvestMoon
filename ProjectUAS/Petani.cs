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
    public partial class Petani : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Petani()
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
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select Id_Petani, Nama_Petani, Alamat, No_HP from dbo.Petani";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void btnPetani_Click(object sender, EventArgs e)
        {
            refreshform();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idPetani = idp.Text;
            string nmPetani = nmp.Text;
            string alPetani = alp.Text;
            string noPetani = nop.Text;
            if (idPetani == "")
            {
                MessageBox.Show("Masukkan Id Petani", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (nmPetani == "")
            {
                MessageBox.Show("Masukkan Nama Petani", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "Insert Into Petani (Id_Petani, Nama_Petani, Alamat, No_HP) values (@idp, @nmp, @alp, @nop)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@idp", idPetani));
                cmd.Parameters.Add(new SqlParameter("@nmp", nmPetani));
                cmd.Parameters.Add(new SqlParameter("@alp", alPetani));
                cmd.Parameters.Add(new SqlParameter("@nop", noPetani));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            idp.Enabled = true;
            nmp.Enabled = true;
            alp.Enabled = true;
            nop.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idPetani = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Petani where Id_Petani = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idPetani);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Pangan tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Id Petani?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Petani where Id_Petani = @idp";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@idp", idPetani);

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
            string idPetani = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Id_Petani, Nama_Petani, Alamat, No_HP From Petani Where Id_Petani = @idp ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@idp", idPetani);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Id Petani tidak ditemukan.");

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
            string idPetani = idp.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Petani where Id_Petani = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idPetani);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Petani tidak ditemukan.");
                        return;
                    }
                }
                string idPetani2 = idp.Text;
                string nmPetani = nmp.Text;
                string alPetani = alp.Text;
                string noPetani = nop.Text;
                string str2 = "Update Petani set Id_Petani = @idp, Nama_Petani = @nmp, Alamat = @alp, No_HP = @nop where Id_Petani = @idp";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@idp", idPetani2));
                cmd2.Parameters.Add(new SqlParameter("@nmp", nmPetani));
                cmd2.Parameters.Add(new SqlParameter("@alp", alPetani));
                cmd2.Parameters.Add(new SqlParameter("@nop", noPetani));
                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Petani berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate data Petani.");
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
                idp.Text = row.Cells["Id_Petani"].Value.ToString();
                nmp.Text = row.Cells["Nama_Petani"].Value.ToString();
                alp.Text = row.Cells["Alamat"].Value.ToString();
                nop.Text = row.Cells["No_HP"].Value.ToString();
            }
        }
    }
}
