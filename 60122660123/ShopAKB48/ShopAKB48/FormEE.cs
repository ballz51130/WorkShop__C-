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
    public partial class FormEE : Form
    {
        public FormEE()
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

        private void Cleardata()
        {


        }
        #endregion

        private void Browes_Click(object sender, EventArgs e)
        {

        }

        private void FormEE_Load(object sender, EventArgs e)
        {
            FilDGV();
            
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
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("กรูณาใส่รูป !!! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();
                SqlCommand command = new SqlCommand("Insert into Product(P_Name,P_Product,P_Price,P_Num,P_Photo,Date)VALUES(@Name, @Dec, @Price, @Num, @Photo, @date)", cn);
               // command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = txtID.Text.Trim();
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text.Trim();
                command.Parameters.Add("@Dec", SqlDbType.Text).Value = textBox2.Text.Trim();
                command.Parameters.Add("@Price", SqlDbType.Int).Value = textBox3.Text.Trim();
                command.Parameters.Add("@Num", SqlDbType.NChar).Value = textBox4.Text.Trim();
                command.Parameters.Add("@Photo", SqlDbType.Image).Value = img;
                command.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();

                ExecMyquery(command, "เพื่มข้อมูลเรียบร้อย");


            }
        }
        public void ExecMyquery(SqlCommand mcond, string myMsg)
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
            if (mcond.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(myMsg);

            }
            else
            {
                MessageBox.Show("query not Excuted");
            }
            cn.Close();
            FilDGV();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            SqlCommand command = new SqlCommand("UPDATE Product SET P_Name = @Name,P_Product= @Dec,P_Price= @Price,P_Num= @Num,P_Photo= @Photo,Date= @Date WHERE P_ID =@ID", cn);
            command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = txtID.Text.Trim();
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text.Trim();
            command.Parameters.Add("@Dec", SqlDbType.Text).Value = textBox2.Text.Trim();
            command.Parameters.Add("@Price", SqlDbType.Int).Value = textBox3.Text.Trim();
            command.Parameters.Add("@Num", SqlDbType.NChar).Value = textBox4.Text.Trim();
            command.Parameters.Add("@Photo", SqlDbType.Image).Value = img;
            command.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();

            ExecMyquery(command, "UPDATE COMPLEASE");


        }

        private void button4_Click(object sender, EventArgs e)
        {

            SqlCommand command = new SqlCommand("DELETE FROM Product WHERE P_ID =@ID", cn);
            command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = txtID.Text.Trim();
            ExecMyquery(command, "DELETE COMPLEASE");

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin mm = new FormAdmin();
            mm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttSrc_Click(object sender, EventArgs e)
        {
            if (textSrc.Text == "")
            {
                MessageBox.Show("กรูณาป้อนคำที่ต้องการค้นหา !!! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("กรุณาเลีอกรายการที่ต้องการค้นหา !!! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          else
            {
               
                DataTable sdt = new DataTable();
                if (radioButton1.Checked == true)
                {
                    string sql = @"select Product.P_ID,Product.P_Name,Product.P_Product,Product.P_Price,Product.P_Num,Product.P_Photo
                        FROM Product where Product.P_ID =" + textSrc.Text.Trim();
                    com.CommandType = CommandType.Text;
                    com.CommandText = sql;
                    com.Connection = cn;
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        sdt.Load(dr);
                        dataGridView1.DataSource = sdt;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                    dr.Close();
                }
                else if (radioButton2.Checked == true)
                {
                    string sql = @"select Product.P_ID,Product.P_Name,Product.P_Product,Product.P_Price,Product.P_Num,Product.P_Photo
                        FROM Product where Product.P_Name Like '" + textSrc.Text.Trim() + "%'";
                    com.CommandType = CommandType.Text;
                    com.CommandText = sql;
                    com.Connection = cn;
                    dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        sdt.Load(dr);
                        dataGridView1.DataSource = sdt;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                    }
                    dr.Close();
                }
                else
                {
                    MessageBox.Show("Insert KeyWord !!! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            txtID.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textSrc.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;



        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            txtID.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textSrc.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            pictureBox1.Image = null;
            this.LoadData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textSrc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
