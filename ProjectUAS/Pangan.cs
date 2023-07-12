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
    public partial class Pangan : Form
    {
        private string stringconnection = "data source = LAPTOP-N8UQTM32\\RENARRO_23;database=ProjectUAS;User ID = sa; Password = dewobroto123";
        private SqlConnection koneksi;
        public Pangan()
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
            cbxjp.Enabled = false;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnSearch.Enabled = false;
            btnUpdate.Enabled = false;
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select Id_Pangan, Jenis_Pangan, Nama_Pangan from dbo.Pangan";
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string idPangan = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Id_Pangan, Jenis_Pangan, Nama_Pangan From Pangan Where Id_Pangan = @idp ";

                SqlCommand cmd = new SqlCommand(str,koneksi);
                cmd.Parameters.AddWithValue("@idp", idPangan);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Id Pangan tidak ditemukan.");

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            idp.Enabled = true;
            nmp.Enabled = true;
            cbxjp.Enabled = true;
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
            btnSearch.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string idPangan = idp.Text;
            string jpPangan = cbxjp.Text;
            string nmPangan = nmp.Text;           
            if (idPangan == "")
            {
                MessageBox.Show("Masukkan Id Pangan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (jpPangan == "")
            {
                MessageBox.Show("Masukkan Jenis Pangan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            else
            {
                koneksi.Open();
                string str = "Insert Into Pangan (Id_Pangan, Jenis_Pangan, Nama_Pangan) values (@idp, @jpp, @nmp)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.Parameters.Add(new SqlParameter("@idp", idPangan));
                cmd.Parameters.Add(new SqlParameter("@jpp", jpPangan));
                cmd.Parameters.Add(new SqlParameter("@nmp", nmPangan));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshform();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idPangan = idp.Text;
            try
            {
                koneksi.Open();
                string str = "Select Count(*) From Pangan where Id_Pangan = @idp";
                using (SqlCommand cmd = new SqlCommand(str, koneksi))
                {
                    cmd.Parameters.AddWithValue("@idp", idPangan);
                    int existingCount = (int)cmd.ExecuteScalar();
                    if(existingCount == 0)
                    {
                        MessageBox.Show("Id Pangan tidak ditemukan.");
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus Id Pangan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result == DialogResult.Yes)
                    {
                        string str2 = "Delete from Pangan where Id_Pangan = @idp";
                        SqlCommand cmd2 = new SqlCommand(str2, koneksi);
                        cmd2.Parameters.AddWithValue("@idp", idPangan);

                        int rowsAffected = cmd2.ExecuteNonQuery();
                        if(rowsAffected > 0)
                        {
                            MessageBox.Show("Id Pangan berhasil dihapus.");
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            string queryString = "Update dbo.Pangan set Id_Pangan='" + idp.Text + "', Jenis_Pangan='" + cbxjp.Text + "', Nama_Pangan ='" + nmp.Text + "";
            SqlCommand cmd = new SqlCommand(queryString, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            koneksi.Close();
            MessageBox.Show("Update Data Berhasil");
            dataGridView();
            refreshform();
        }
    }
}
