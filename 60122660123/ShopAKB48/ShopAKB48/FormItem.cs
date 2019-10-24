using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;


namespace ShopAKB48
{
    public partial class FormItem : Form
    {
        public FormItem()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlTransaction tr;
        DataTable mt = new DataTable();
        DataTable mem = new DataTable();
        
        #region
        private void LoadData()
        {
            mem.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string sql = @"select  * from Product";
            SqlDataReader dr;
            com = new SqlCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = sql;
            com.Connection = cn;
            dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                mt = new DataTable();
                mt.Load(dr);
                dataGridView1.DataSource = mt;

            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
            }
            dr.Close();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].HeaderText = "รหัสสินค้า";
            dataGridView1.Columns[1].HeaderText = "ชื่อสินค้า";
            dataGridView1.Columns[2].HeaderText = "รายระเอียด";
            dataGridView1.Columns[3].HeaderText = "ราคา";
            dataGridView1.Columns[4].HeaderText = "จำนวน";
            dataGridView1.Columns[5].HeaderText = "รูปภาพ";
            dataGridView1.Columns[6].HeaderText = "วันที่";


        }
        #endregion
        private void FormItem_Load(object sender, EventArgs e)
        {
            FilDGV();
        }
        public void FilDGV()
        {
           
            SqlCommand command = new SqlCommand("SELECT * FROM Product", cn);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[5];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[5].Value;
            MemoryStream ms = new MemoryStream(img);

            pictureBox1.Image = Image.FromStream(ms);
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin mm = new FormAdmin();
            mm.Show();
        }
    }
}
