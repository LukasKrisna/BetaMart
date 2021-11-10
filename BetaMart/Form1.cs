using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BetaMart
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = new OleDbConnection();

        public Form1()
        {
            InitializeComponent();
            koneksi kon = new koneksi();
            connection.ConnectionString = kon.con;
        }

        string query;

        #region Load
        private void Form1_Load(object sender, EventArgs e)
        {
            dGV.Rows.Clear();
            GetData();
        }
        #endregion

        #region Get Data
        private void GetData()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            query = "select * from barang";
            command.CommandText = query;

            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataSet ds = new DataSet();
            da.Fill(ds);

            int cnt = ds.Tables[0].Rows.Count;
            dGV.Rows.Add(cnt);

            int no = 0;

            for (int i = 0; i < cnt; i++)
            {
                no = no + 1;
                DataRow row = ds.Tables[0].Rows[i];
                dGV.Rows[i].Cells[0].Value = no.ToString();
                dGV.Rows[i].Cells[1].Value = row.ItemArray.GetValue(0).ToString();
                dGV.Rows[i].Cells[2].Value = row.ItemArray.GetValue(1).ToString();
                dGV.Rows[i].Cells[3].Value = row.ItemArray.GetValue(2).ToString();
                dGV.Rows[i].Cells[4].Value = Convert.ToDateTime(row.ItemArray.GetValue(3).ToString()).ToString("dd MMMM yyyy");
                dGV.Rows[i].Cells[5].Value = row.ItemArray.GetValue(4).ToString();
                dGV.Rows[i].Cells[6].Value = row.ItemArray.GetValue(5).ToString();
                dGV.Rows[i].Cells[7].Value = row.ItemArray.GetValue(6).ToString();
            }

            connection.Close();
        }
        #endregion

        #region Add Data
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                query = "INSERT INTO `barang` (`kode`, `jenis_brg`, `merk`, `tgl_input`, `kondisi`, `operator`, `keterangan`)" +
                "VALUES ('" + txtKode.Text.ToUpper() + "', '" + txtJenis.Text.ToUpper() + "', '" + txtMerk.Text.ToUpper() + "', '" + dtpTanggal.Value.ToString("dd/MM/yyyy") + "', '" + cmbKondisi.Text.ToUpper() + "', '" + txtKeterangan.Text.ToUpper() + "', '" + txtOperator.Text.ToUpper() + "');";
                command.CommandText = query;

                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil ditambah!");
                connection.Close();

                txtKode.Clear();
                txtJenis.Clear();
                txtMerk.Clear();
                dtpTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                cmbKondisi.Text = "SEGEL UTUH";
                txtKeterangan.Clear();
                txtOperator.Clear();

                dGV.Rows.Clear();
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }
        #endregion

        #region Edit Data
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                query = "UPDATE `barang` SET jenis_brg ='" + txtJenis.Text.ToUpper() + "', merk ='" + txtMerk.Text.ToUpper() + "', tgl_input ='" + dtpTanggal.Value.ToString("dd/MM/yyyy") + "'," +
                "kondisi ='" + cmbKondisi.Text.ToUpper() + "', operator ='" + txtKeterangan.Text.ToUpper() + "', keterangan ='" + txtOperator.Text.ToUpper() + "' WHERE kode = '" + txtKode.Text.ToUpper() + "';";
                command.CommandText = query;

                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil diubah!");
                connection.Close();

                txtKode.Clear();
                txtJenis.Clear();
                txtMerk.Clear();
                dtpTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                cmbKondisi.Text = "SEGEL UTUH";
                txtKeterangan.Clear();
                txtOperator.Clear();

                dGV.Rows.Clear();
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }
        #endregion

        #region Delete Data
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                query = "DELETE FROM barang WHERE kode = '" + txtKode.Text.ToUpper() + "';";
                command.CommandText = query;

                command.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus!");
                connection.Close();

                txtKode.Clear();
                txtJenis.Clear();
                txtMerk.Clear();
                dtpTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
                cmbKondisi.Text = "SEGEL UTUH";
                txtKeterangan.Clear();
                txtOperator.Clear();

                dGV.Rows.Clear();
                GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }
        #endregion

    }
}