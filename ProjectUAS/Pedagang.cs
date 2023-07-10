﻿using System;
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
            btnClear.Enabled = false;
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
            btnClear.Enabled = true;
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
            if (alPedagang == "")
            {
                MessageBox.Show("Masukkan Alamat Pedagang", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (noPedagang == "")
            {
                MessageBox.Show("Masukkan Nomor HP Pedagang", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
