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
    public partial class register : Form
    {



        Connection c = new Connection();
        Bean b = new Bean();


        public register()
        {
            InitializeComponent();
        }

        private void register_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //用户名注册
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //密码注册
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            b.un = textBox1.Text.ToString();
            b.pd = textBox2.Text.ToString();
            int i=c.register(b.un,b.pd);
            if (i == 1)
            {
                MessageBox.Show("注册成功");
                Form1 f = new Form1();
                f.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("注册失败");
                textBox1.Text = "";
            }
        }
    }
}
