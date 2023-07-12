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
    public partial class Kurir : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Kurir()
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
            cbxjk.Enabled = false;
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
            string str = "select Id_Kurir, Nama_Kurir, Jenis_Kelamin, No_HP from dbo.Kurir";
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            idp.Enabled = true;
            nmp.Enabled = true;
            cbxjk.Enabled = true;
            nop.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idKurir= idp.Text;
            string nmKurir = nmp.Text;
            string jkKurir = cbxjk.Text;
            string noKurir = nop.Text;
            if (idKurir == "")
            {
                MessageBox.Show("Masukkan Id Kurir", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (nmKurir == "")
            {
                MessageBox.Show("Masukkan Nama Kurir", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (jkKurir == "")
            {
                MessageBox.Show("Masukkan Jenis Kelamin Kurir", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (noKurir == "")
            {
                MessageBox.Show("Masukkan Nomor HP Kurir", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "Insert Into Kurir (Id_Kurir, Nama_Kurir, Jenis_Kelamin, No_HP) values (@idp, @nmp, @alp, @nop)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@idp", idKurir));
                cmd.Parameters.Add(new SqlParameter("@nmp", nmKurir));
                cmd.Parameters.Add(new SqlParameter("@alp", jkKurir));
                cmd.Parameters.Add(new SqlParameter("@nop", noKurir));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idKurir = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Kurir where Id_Kurir = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idKurir);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Pengepul tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Id Kurir?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Kurir where Id_Kurir = @idp";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@idp", idKurir);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Id Pengepul berhasil dihapus.");
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
            string idKurir = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Id_Kurir, Nama_Kurir, Jenis_Kelamin, No_HP From Kurir Where Id_Kurir = @idp ";

                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.AddWithValue("@idp", idKurir);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Id Kurir tidak ditemukan.");

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
            string idKurir = idp.Text.Trim();

            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Kurir where Id_Kurir = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idKurir);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if (existingCount == 0)
                    {
                        MessageBox.Show("Id Kurir tidak ditemukan.");
                        return;
                    }
                }
                string idKurir2 = idp.Text;
                string nmKurir = nmp.Text;
                string jkKurir = cbxjk.Text;
                string noKurir = nop.Text;
                string str2 = "Update Kurir set Id_Kurir = @idp, Nama_Kurir = @nmp, Jenis_Kelamin = @alp, No_HP = @nop where Id_Kurir= @idp";
                SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                cmd2.Parameters.Add(new SqlParameter("@idp", idKurir2));
                cmd2.Parameters.Add(new SqlParameter("@nmp", nmKurir));
                cmd2.Parameters.Add(new SqlParameter("@alp", jkKurir));
                cmd2.Parameters.Add(new SqlParameter("@nop", noKurir));
                int rowsAffected = cmd2.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data Kurir berhasil diupdate.");
                }
                else
                {
                    MessageBox.Show("Gagal mengupdate Data Kurir.");
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
