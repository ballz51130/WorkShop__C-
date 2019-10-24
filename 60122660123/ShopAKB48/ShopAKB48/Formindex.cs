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
    public partial class Formindex : Form
    {
        public Formindex(string value)
        {
            InitializeComponent();
            UserID.Text = value;
        }
        
        SqlConnection cn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlTransaction tr;
        DataTable mt = new DataTable();
        DataTable mem = new DataTable();
        #region
        private void lsv()
        {
            listView1.Columns.Add("รหัสสินค้า", 75);
            listView1.Columns.Add("ชื่อสินค้า", 100);
            listView1.Columns.Add("รายระเอียด", 160);
            listView1.Columns.Add("จำนวน", 80);
            listView1.Columns.Add("ราคาต่อหน่วย", 90);
            listView1.Columns.Add("รวมเป็นเงิน", 100);
            listView1.View = View.Details;
            listView1.GridLines = true;

        }
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
            dataGridView1.Columns[6].HeaderText = "วันลงสินค้า";


        }
        private void Cleardata()
        {


        }
        #endregion
        private void Formindex_Load(object sender, EventArgs e)
        {
            FilDGV();
            string strConn;
            strConn = ConnectDatabase.AKBSHOP;
            cn = new SqlConnection();
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.ConnectionString = strConn;
            cn.Open();
            this.lsv();
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
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[5];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            float num=0, price=0, sum=0;

            if (textBox4.Text != "0")
            {
                num = float.Parse(textBox4.Text);
                price = float.Parse(textBox3.Text);
                sum = num * price;
                textBox5.Text = sum.ToString("#,##0.00");

            }
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
         
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("กรุณากรอกข้อมูลรหัสสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }
            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                int Item = Convert.ToInt32(listView1.Items[i].SubItems[0].Text);
                if (Convert.ToInt32(txtID.Text) == Item)
                {
                    MessageBox.Show("คุณเลีอกรายการซ้ำกัน", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox1.Focus();
                    return;

                }
            }
            string[] Indata;
            float x, y, z, t, m;
            if (textBox6.Text == "")
            {
                textBox6.Text = "0";
            }
            x = float.Parse(textBox4.Text);
            y = float.Parse(textBox3.Text);
            m = float.Parse(textBox6.Text);
            z = x * y;
            t = m + z;
            textBox6.Text = t.ToString("#,##0.00");
            Indata = new string[] { txtID.Text,textBox1.Text,textBox2.Text,textBox4.Text,textBox3.Text,z.ToString()
            };
            ListViewItem liv = new ListViewItem(Indata);
            listView1.Items.Add(liv);
            this.Cleardata();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cleardata();
            textBox6.Text = "0.00";
            listView1.Clear();
            this.lsv();
        }

        private void button3_Click(object sender, EventArgs e)
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
            if (MessageBox.Show("ต้องการสั่งซื้อหรื่อไม่", "Applying", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
            {
                tr = cn.BeginTransaction();
                try
                {
                    for (int i = 0; i <= listView1.Items.Count - 1; i++)
                    {
                        String sql = @"INSERT INTO OrderProduct(S_UserName,S_ID,S_Name,S_Num,S_Price,S_Sum,Date)VALUES(@UserName,@O_ID,@O_Name,@O_Num,@O_Price,@O_Sum,@Date)";
                        com.Parameters.Clear();
                        com.CommandText = sql;
                        com.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserID.Text.Trim();
                        com.Parameters.Add("@O_ID", SqlDbType.Int).Value = (listView1.Items[i].SubItems[0].Text);
                        com.Parameters.Add("@O_Name", SqlDbType.NVarChar).Value =(listView1.Items[i].SubItems[2].Text);
                        com.Parameters.Add("@O_Num", SqlDbType.Int).Value = int.Parse(listView1.Items[i].SubItems[3].Text);
                        com.Parameters.Add("@O_Price", SqlDbType.Float).Value = float.Parse(listView1.Items[i].SubItems[4].Text);
                        com.Parameters.Add("@O_Sum", SqlDbType.Float).Value = float.Parse(listView1.Items[i].SubItems[5].Text);
                        com.Parameters.Add("Date", SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
                        com.Transaction = tr;
                        com.ExecuteNonQuery();
                        

                    }
                    tr.Commit(); this.Cleardata(); listView1.Clear(); this.lsv(); textBox6.Text = "0.00";

                    MessageBox.Show("ทำการสั่งซื้อสำเร็จแล้ว ", "Processing result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  //  txtPID.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ไม่สามารถบันทึกได้ " + ex.Message, "Proceesing result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tr.Rollback();
                }
            }
        }

        private void label10_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtID.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0.00";
            pictureBox1.Image = null;
            dataGridView1.Refresh();




        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 mm = new Form1();
            mm.Show();
        }
    }
}
