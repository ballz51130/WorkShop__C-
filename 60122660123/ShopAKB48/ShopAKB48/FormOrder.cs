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
    public partial class FormOrder : Form
    {
        public FormOrder()
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
        private void LoadData2()
        {
           
            mem.Clear();
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            string sql = @"select  * from OrderProduct";
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
                dataGridView2.DataSource = mt;

            }
            else
            {
                dataGridView2.DataSource = null;
                dataGridView2.Refresh();
            }
            dr.Close();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns[0].HeaderText = "ลำดับ";
            dataGridView2.Columns[1].HeaderText = "ไอดีลูกค้า";
            dataGridView2.Columns[2].HeaderText = "รหัสสินค้า";
            dataGridView2.Columns[3].HeaderText = "ชื่อสินค้า";
            dataGridView2.Columns[4].HeaderText = "จำนวน";
            dataGridView2.Columns[5].HeaderText = "ราคาต่อชิ้น";
            dataGridView2.Columns[6].HeaderText = "ราคารวม";
            dataGridView2.Columns[7].HeaderText = "ว/ด/ป";


        }
      
        private void Member()
        {
            mem.Clear();
            dataGridView3.DataSource = null;
            dataGridView3.Rows.Clear();
            dataGridView3.Refresh();
            string sql = @"select  * from User_IDN";
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
                dataGridView3.DataSource = mt;

            }
            else
            {
                dataGridView3.DataSource = null;
                dataGridView3.Refresh();
            }
            dr.Close();
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.Columns[0].HeaderText = "ID";
            dataGridView3.Columns[1].HeaderText = "ไอดีลูกค้า";
            dataGridView3.Columns[3].HeaderText = "รหัส";
            dataGridView3.Columns[2].HeaderText = "Email";
            dataGridView3.Columns[4].HeaderText = "ชื่อ-สกุล";
            dataGridView3.Columns[5].HeaderText = "ที่อยู่";
            dataGridView3.Columns[6].HeaderText = "เบอร์โทร";


        }
        private void Cleardata()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox2.Focus();
            mt.Clear();


        }
        #endregion
        private void FormOrder_Load(object sender, EventArgs e)
        {
            
            this.FilDGV();
            string strConn;
            strConn = ConnectDatabase.AKBSHOP;
            cn = new SqlConnection();
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.ConnectionString = strConn;
            cn.Open();
            this.LoadData2();
            this.Member();
        }
            public void FilDGV()
            {
                string strConn;
                strConn = ConnectDatabase.AKBSHOP;
                cn = new SqlConnection();
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.ConnectionString = strConn;
                cn.Open();
                this.LoadData();
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
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Delete Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

            {
                string strConn;
                strConn = ConnectDatabase.AKBSHOP;
                cn = new SqlConnection();
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.ConnectionString = strConn;
                cn.Open();
                this.LoadData2();
                tr = cn.BeginTransaction();
                try
                {
                   
                    string sql = @"Delete from OrderProduct Where ID=@ID";
                    com.CommandText = sql;
                    com.CommandType = CommandType.Text;
                    com.Connection = cn;
                    com.Transaction = tr;
                    com.Parameters.Clear();
                    com.Parameters.Add("@ID", SqlDbType.Int).Value = textBox1.Text.Trim();
                    com.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show("Sucressfull !!!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                this.LoadData2();
                

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("คุณต้องการแก้ไขสินค้าหรือไม่", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

            {
                string strConn;
                strConn = ConnectDatabase.AKBSHOP;
                cn = new SqlConnection();
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.ConnectionString = strConn;
                cn.Open();
                this.LoadData();
                tr = cn.BeginTransaction();

                try
                {
                   
                    string sql = @"UPDATE Product SET P_Num = @Num where P_ID = @ID ";
                    com.CommandText = sql;
                    com.CommandType = CommandType.Text;
                    com.Connection = cn;
                    com.Transaction = tr;
                    com.Parameters.Clear();
                    com.Parameters.Add("@ID", SqlDbType.NChar).Value = textBox3.Text.Trim();
                    com.Parameters.Add("@Num", SqlDbType.NChar).Value = textBox5.Text.Trim();
                    com.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show("Sucressfull !!!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.LoadData();
                    

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin mm = new FormAdmin();
            mm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
        }
    }
}
