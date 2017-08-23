using DataBaseClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace BMS
{

    public struct Bean {

        public string un;                                    //用户名                       
        public string pd;                                    //密码
        public string UseName;                          
        public string Passwd;
        public string TodayDate;                             //当前日期
        public string ProductName;                           //产品名称
        public string Shift;                                 //班次
        public string Mark;                                  //正次品标志        
        public string CommodityCode;                         //品名码
        public string OrdinalNum;                           //来源码
        public string HeatNum;                              //炉号
        public string Weight;                               //重量
        public string TotalNum;                             //本班总吊数
        public string Length;                               //长度
        public string Brand;                                //牌号
        public string Source;                               //来源
        public string TotalWeight;                          //本炉号总重量

    }

    class Connection
    {

        DbSql db = new DbSql();
  

        public Connection()
        {
            link();
        }

        public void link()
        {
            try
            {
                ConnectionString.Conntype = SType.ConnType.None;
                ConnectionString.Sqltype = SType.SqlType.SqlServer;
                ConnectionString con1 = new ConnectionString();
                con1.DataSource = "SC-201703300754";
                con1.InitialCatalog = "db_BMS";
                con1.UserID = "sa";
                con1.Password = "sun123456";
                db.StrCon = con1.GetSetting;

            }
            catch (Exception )
            {

            }
        }


        //注册用户
        public int register(string un, string pd)
        {
            DataTable u = new Connection().User(un);
            if (u.Rows.Count == 0)
            {
                string s1 = "insert into [db_BMS].[dbo].[db_User](UseName,Passwd) values ('" + un + "','" + pd + "')";
                return db.ExecuteNonQuery(s1);
            }
            else {
                return 10;
            }
        }



        //查询用户名
        public DataTable User(string un) {
            string sql = "select * from [db_BMS].[dbo].[db_User] where useName='"+ un +"'";
            
            return db.ExecuteDataTable(sql);
        }


        //查询密码
        public DataTable Pwd(string pd) {
            string sql = "select * from [db_BMS].[dbo].[db_User] where Passwd='" + pd + "'";
            
            return db.ExecuteDataTable(sql);
        }



        //手动添加信息
        public int AddBMS(string TodayDate, string ProductName, string Shift, string Mark, string CommodityCode, string OrdinalNum, string HeatNum, string Weight, string TotalNum, string Length, string Brand, string Source, string TotalWeight)
        {
            string s = "insert into [db_BMS].[dbo].[tb_Bms](TodayDate,ProductName,Shift,Mark,CommodityCode,OrdinalNum,HeatNum,Weight,TotalNum,Length,Brand,Source,TotalWeight) values ('" +
                TodayDate + " ','" + ProductName + " ','" + Shift + " ','" + Mark + " ','" + CommodityCode + " ','" + OrdinalNum + " ','" + HeatNum + " ','" + Weight + " ','" + TotalNum + "','" + Length + "','" + Brand + "','" + Source + "','" + TotalWeight + "')";
            return db.ExecuteNonQuery(s);
        }


        //查询所有数据
        public DataTable table(DateTime tdt)
        {
            //获取当前日期  通过当前日期去获取这一天的所有添加的信息 显示在表格中 从而方便导入excel中
            DateTime d = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            //获取当前日期的后一天日期  作为一整天的范围
            DateTime d1 = d.AddDays(1);
            string s = "select * from [db_BMS].[dbo].[tb_Bms] where TodayDate between '"+d+"' and '"+ d1 + "'";

            return db.ExecuteDataTable(s);
        }
















    }
}
