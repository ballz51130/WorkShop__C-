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
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        private Formindex UID;
        private void button1_Click(object sender, EventArgs e )
        {
            
            SqlConnection conn = new SqlConnection(@"Data Source = HARUMYX; Initial Catalog = AKBSHOP48; Integrated Security = True; Connect Timeout = 30");
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from User_IDN where U_UserName ='" + UsID.Text + "' and U_Password='" + textBox2.Text + "'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           
                if (dt.Rows[0][0].ToString() == "1")
                {
                if (UsID.Text == "GG")
                {
                    this.Hide();
                    FormAdmin mm = new FormAdmin();
                    mm.Show();
                }
                    else
                    {
                    
                        this.Hide();
                        Formindex mm = new Formindex(UsID.Text);
                        mm.Show();
                   
                    }

                }
            
            else
            {
                MessageBox.Show("please enter correct username and password", "alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRegister mm = new FormRegister();
            mm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
