using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMS
{
    public partial class Form1 : Form
    {
        Connection c = new Connection();
        Bean b = new Bean();
        //*************
        public string UseName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
 
        }

        //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
           
        //}


            //用户名的输入框
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    UseName = textBox2.Text.ToString();
            //    Passwd = textBox3.Text.ToString();
            //    c.BMS1(UseName, Passwd);
            //    c.AddBMS(UseName, TodayDate, ProductName, Shift, Mark, CommodityCode, OrdinalNum, HeatNum, Weight, TotalNum, Length, Brand, Source, TotalWeight);
            //}
        }


            //密码输入框
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
          
        }


        //注册按钮
        private void button1_Click_1(object sender, EventArgs e)
        {
            register reg = new register();
            reg.Show();
            this.Hide();
        }


        //登陆按钮
        public void 登录_Click(object sender, EventArgs e)
        {
            UseName= textBox2.Text.ToString();
            b.un = textBox2.Text.ToString();
            b.pd = textBox3.Text.ToString();
            DataTable u = c.User(b.un);
            DataTable p = c.Pwd(b.pd);
            
 //           textBox2.Text = p.Rows[0][0].ToString();
            if (u.Rows.Count > 0)
            {

                if (p.Rows.Count > 0)
                {
                    MessageBox.Show("登陆成功");
                    BMS bms = new BMS();
                    bms.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
            }
            else
            {
                MessageBox.Show("该用户未注册");
            }



        }
    }
}


