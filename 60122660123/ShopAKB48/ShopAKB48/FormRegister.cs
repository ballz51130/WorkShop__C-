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
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            string strConn;
            strConn = ConnectDatabase.AKBSHOP;
            cn = new SqlConnection();
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.ConnectionString = strConn;
            cn.Open();
           
        }
        SqlConnection cn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlTransaction tr;
        DataTable mt = new DataTable();
        DataTable mem = new DataTable();


        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการสมัครสมาชิกใหม่?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)

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
                    com.Parameters.Add("@Type", SqlDbType.NChar).Value = "member";
                    com.Parameters.Add("@Email", SqlDbType.NVarChar).Value = T_Email.Text.Trim();
                    com.Parameters.Add("@Name", SqlDbType.NVarChar).Value = T_Name.Text.Trim();
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

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 mm = new Form1();
            mm.Show();
        }
    }
}
