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
namespace ShopAKB48
{
    public partial class FormMember : Form
    {
        public FormMember()
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
                dataGridView1.DataSource = mt;

            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
            }
            dr.Close();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "User";
            dataGridView1.Columns[2].HeaderText = "Email";
            dataGridView1.Columns[3].HeaderText = "รหัส";
            dataGridView1.Columns[4].HeaderText = "ชื่อ-สกุล";
            dataGridView1.Columns[5].HeaderText = "ที่อยู่";
            dataGridView1.Columns[6].HeaderText = "เบอร์โทร";
         

        }
       
        private void Cleardata()
        {
           T_Name.Clear();
            T_Email.Clear();
            T_Password.Clear();
            T_Phone.Clear();
            T_Zip.Clear();
            textBox1.Clear();
            dataGridView1.Refresh();
            textBox5.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;



        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormAdmin mm = new FormAdmin();
            mm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการเพื่มสมัครสมาชิกใหม่?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

            {
                tr = cn.BeginTransaction();
                try
                {
                    string sql = @"Insert into User_IDN (U_UserName,U_Password,Type,U_Email,U_Name,U_Address,U_Phone)VALUES(@User,@Pass,@Type,@Email,@Name,@add,@phone)";
                    com.CommandText = sql;
                    com.CommandType = CommandType.Text;
                    com.Connection = cn;
                    com.Transaction = tr;
                    com.Parameters.Clear();
                    com.Parameters.Add("@User", SqlDbType.NVarChar).Value = T_Name.Text.Trim();
                    com.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = T_Password.Text.Trim();
                    com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = T_Email.Text.Trim();
                    com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text.Trim();
                    com.Parameters.Add("@add", SqlDbType.Text).Value = T_Zip.Text.Trim();
                    com.Parameters.Add("@phone", SqlDbType.NVarChar).Value = T_Phone.Text.Trim();
                    com.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show("Sucressfull !!!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form1 mm = new Form1();
                    mm.Show();


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormMember_Load(object sender, EventArgs e)
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("UPDATEMEMBER?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

            {
                tr = cn.BeginTransaction();

                try
                {
                    string sql = @"UPDATE User_IDN SET U_UserName = @Name ,U_Email = @Email ,U_Password = @Pass,U_Name = @T_Name,U_Address = @Add, U_Phone = @Phone WHERE A_ID = @ID";
                    com.CommandText = sql;
                    com.CommandType = CommandType.Text;
                    com.Connection = cn;
                    com.Transaction = tr;
                    com.Parameters.Clear();
                    com.Parameters.Add("@ID", SqlDbType.NVarChar).Value = T_ID.Text.Trim();
                    com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = T_Name.Text.Trim();
                    com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = T_Email.Text.Trim();
                    com.Parameters.Add("@Pass", SqlDbType.NVarChar).Value = T_Password.Text.Trim();
                    com.Parameters.Add("@T_Name", SqlDbType.NVarChar).Value = textBox1.Text.Trim();
                    com.Parameters.Add("@Add", SqlDbType.Text).Value = T_Zip.Text.Trim();
                    com.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = T_Password.Text.Trim();
                   
                    com.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show("Sucressfull !!!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cleardata();
                    this.LoadData();

                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            T_ID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            T_Name.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            T_Password.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            T_Email.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            T_Phone.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            T_Zip.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete member?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

            {
                tr = cn.BeginTransaction();

                try
                {
                    string sql = @"Delete from User_IDN Where A_ID=@ID";
                    com.CommandText = sql;
                    com.CommandType = CommandType.Text;
                    com.Connection = cn;
                    com.Transaction = tr;
                    com.Parameters.Clear();
                    com.Parameters.Add("@ID", SqlDbType.Int).Value = T_ID.Text.Trim();
                    com.ExecuteNonQuery();
                    tr.Commit();
                    MessageBox.Show("Sucressfull !!!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Cleardata();
                this.LoadData();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Insert KeyWord !!! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                DataTable sdt = new DataTable();
                if (radioButton1.Checked == true)
                {
                    string sql = @"select User_IDN.A_ID,User_IDN.U_UserName,User_IDN.U_Email,User_IDN.U_Password,User_IDN.U_Name,User_IDN.U_Address,User_IDN.U_Phone FROM User_IDN WHERE A_ID =" + textBox5.Text.Trim();
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

                    string sql = @"select User_IDN.A_ID,User_IDN.U_UserName,User_IDN.U_Email,User_IDN.U_Password,User_IDN.U_Name,User_IDN.U_Address,User_IDN.U_Phone
                        FROM User_IDN where U_UserName Like '" + textBox5.Text.Trim() + "%'";
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
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "User";
                dataGridView1.Columns[2].HeaderText = "Email";
                dataGridView1.Columns[3].HeaderText = "รหัส";
                dataGridView1.Columns[4].HeaderText = "ชื่อ-สกุล";
                dataGridView1.Columns[5].HeaderText = "ที่อยู่";
                dataGridView1.Columns[6].HeaderText = "เบอร์โทร";
               

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Cleardata();
            this.LoadData();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Cleardata();
            this.LoadData();
        }
    }
}
