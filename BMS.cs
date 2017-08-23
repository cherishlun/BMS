using client;
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
    public partial class BMS : Form
    {
        //实例化构造体 
        Bean b = new Bean();
        //实例化连接
        Connection c = new Connection();
        //实例化类
        Pro8142 hbm = new Pro8142();


        DataTable dt = new DataTable();

        public BMS()
        {
            InitializeComponent();
            dt.Columns.Add("序号");
            dt.Columns.Add("当前日期");
            dt.Columns.Add("产品名称");
            dt.Columns.Add("班次");
            dt.Columns.Add("正次品标志");
            dt.Columns.Add("品名码");
            dt.Columns.Add("来源码");
            dt.Columns.Add("炉号");
            dt.Columns.Add("重量");
            dt.Columns.Add("本班总吊数");
            dt.Columns.Add("长度");
            dt.Columns.Add("牌号");
            dt.Columns.Add("来源");
            dt.Columns.Add("本炉号总重量");

           
        }

        //获取重量
        private void zl()
        {
            hbm.port = 5;
            hbm.BaudRate = 10;
            hbm.StopBits = 0;
            hbm.Parity = 0;
            hbm.ByteSize = 3;
            hbm.myEvent += new Pro8142.ProDelegate(dzl);
            hbm.Weight_Start();
        }

        //将重量的值传入对应的表格中
        private void dzl(string _zl, int flag)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                textBox3.Text = _zl;
            });
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        //日期
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            b.TodayDate = DateTime.Now.ToString();
            textBox1.Text = b.TodayDate;
        }

        //产品名称
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //炉号
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        //重量
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //本班总吊数
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //班次
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        //正次品标志
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        //长度
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        //牌号
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        //来源
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        //品名码
        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        //来源码
        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        //本炉总重量
        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }


        public void button1_Click(object sender, EventArgs e)
        {
            //b.TodayDate = textBox1.Text.ToString();


            //获取当前时间
            b.TodayDate = DateTime.Now.ToString();
            textBox1.Text = b.TodayDate;
            //获取产品名称
            b.ProductName = textBox4.Text.ToString();
            //获取炉号
            b.HeatNum = textBox5.Text.ToString();
            //重量获取
            b.Weight = textBox3.Text.ToString();
            //本班总吊数
            b.TotalNum = textBox2.Text.ToString();
            //班次
            b.Shift = textBox6.Text.ToString();
            //正次品标志
            b.Mark = textBox7.Text.ToString();
            //长度
            b.Length = textBox9.Text.ToString();
            //牌号
            b.Brand = textBox10.Text.ToString();
            //来源
            b.Source = textBox8.Text.ToString();
            //品名码
            b.CommodityCode = textBox13.Text.ToString();
            //来源码
            b.OrdinalNum = textBox11.Text.ToString();
            //本班总重量
            b.TotalWeight = textBox12.Text.ToString();

//            DataRow dr = dt.NewRow();

                //int j=1;
                
                //dr[0] = j++;

                //dr[1] = b.TodayDate;
                //dr[2] = b.ProductName;
                //dr[3] = b.Shift;
                //dr[4] = b.Mark;
                //dr[5] = b.CommodityCode;
                //dr[6] = b.OrdinalNum;
                //dr[7] = b.HeatNum;
                //dr[8] = b.Weight;
                //dr[9] = b.TotalNum;
                //dr[10] = b.Length;
                //dr[11] = b.Brand;
                //dr[12] = b.Source;
                //dr[13] = b.TotalWeight;
                //dt.Rows.Add(dr);
                //dataGridView1.DataSource = dt;

                //去除最后一行空格
                dataGridView1.AllowUserToAddRows = false;





            int i=c.AddBMS(b.TodayDate,b.ProductName,b.HeatNum,b.Weight,b.TotalNum,b.Shift,b.Mark,b.Length,b.Brand,b.Source,b.CommodityCode,b.OrdinalNum,b.TotalWeight);

            if (i == 1)
            {
                MessageBox.Show("数据添加成功");
            }
            else {

                MessageBox.Show("数据添加失败");
                //不可在这里进行判断  当输入的时间格式有问题时  在Connection.cs中便会出错  无法执行
                //有种方法可以将输入的值全部清空   使用for循环清除
            }

            DateTime tdt = Convert.ToDateTime(b.TodayDate);
            dataGridView1.DataSource = c.table(tdt);

        }

        private void BMS_Load(object sender, EventArgs e)
        {
            zl();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
        }
    }
}
