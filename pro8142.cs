using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MyClass;
namespace client
{
    //托利多（连续方式）与科杰
    public class Pro8142
    {
        public delegate void ProDelegate(String Weight, int flag);
        public event ProDelegate myEvent;
        Thread t = null;
        public bool thread_end = true;
        public int port; //虚拟串口号
        public int BaudRate; //波特率
        public int Parity;// 奇偶校验位
        public int ByteSize;//数据位
        public int StopBits;//停止位
        public int SleepTime; //此参数与波特率有关
        public int flag;
        public int ret;
        public String bytes2HexString(byte[] b)
        {

            String ret = "";

            for (int i = 0; i < b.Length; i++)
            {

                String hex = b[i].ToString("X2");

                if (hex.Length == 1)
                {

                    hex = '0' + hex;

                }

                ret += hex;

            }

            return ret;

        }
        public void OpenPort() //打开串口并读数
        {
            int mode;
            bool bol = false;
            while (bol == false)
            {
                ret = pComm.sio_open(port);
                if (ret < 0)
                {
                    LogClass.WriteLog("weight_log", "称重仪表(sio_open)打开串口,串口号:" + port + "  返回值: " + ret, EFlag.error);
                }
                if (ret == 0)
                {
                    bol = true;
                }
                System.Threading.Thread.Sleep(100);
            }
            while (thread_end)
            {
                mode = BaudRate | StopBits | ByteSize;
              //  ret = pComm.sio_flush(port, 0);
                ret = pComm.sio_ioctl(port, BaudRate, mode);
                System.Threading.Thread.Sleep(100);
                int rlen;
                byte[] buf = new byte[127];
                rlen = pComm.sio_read(port, ref buf[0], 127);
                System.Threading.Thread.Sleep(100);
                //rlen == 0 ||(20150218)
                if ( ret != 0)
                {
                    flag = ret; //中断
                    LogClass.WriteLog("weight_log", "称重仪表读数据(sio_read)返回值,串口号:" + port + "  sio_read 返回值: " + rlen + "sio_ioctl返回值: " + ret, EFlag.error);
                    myEvent("0", rlen);
                }
                else
                {
                    LogClass.WriteLog("weight_log", "称重仪表读数据(sio_read)返回值,串口号:" + port + "  sio_read 返回值: " + rlen + "sio_ioctl返回值: " + ret, EFlag.error);
                    flag = ret; //正常
                   
                }

                ComRead(buf, rlen, flag);

            }
            //时间与波特率有关系
             System.Threading.Thread.Sleep(1000);

        }
        public void ClosePort()
        {
            pComm.sio_close(port);
        }
        public void ComRead(byte[] dat, int rlen, int f)
        {
            int Comflag;
            double zl = 0;
            Comflag = f;
            int i = -1;
            string zlstr="0";
            string aa = "";

            if (dat.Length > 0)
            {
                for (int p = 0; p < dat.Length; p++)
                {
                    aa += dat[p].ToString()+",";
                }

                zlstr = System.Text.Encoding.ASCII.GetString(dat, 0, dat.Length);
                LogClass.WriteLog("weight_log", "称重仪表读数据返回值,串口号:" + port + "  zlstr 返回值: " + aa, EFlag.error);
                string pp = zlstr;
               //    i = zlstr.IndexOf('\u0002');
                i = zlstr.IndexOf(Convert.ToChar(2));
                if (i >= 0)
                {
                    zlstr = zlstr.Substring(i, zlstr.Length - i);
                }

                if (zlstr.Length > 17)
                {
                    zlstr = zlstr.Substring(4, 6);
                    if (zlstr.Trim().Length > 0)
                    {
                        //int m = Convert.ToInt32(dat[i + 2]);
                        //int m1 = 4;
                        //int m2 = m & m1;

                        //if (m2 > 0)
                        //{
                        //    LogClass.WriteLog("weight_log", "称重仪表读数据(zlstr=-1)返回值,串口号:" + port + "  zlstr 返回值: " + pp + "十六进制" + aa, EFlag.error);
                        //    zlstr = "-1";
                        //    //触发事件
                        //    myEvent(zlstr.ToString(), flag);
                        //    return;
                        //}
                        try
                        {
                        
                            zl = Convert.ToInt32(zlstr);

                            myEvent(zl.ToString(), flag);

                        }
                        catch (Exception ex)
                        {
                            flag = -13;
                            LogClass.WriteLog("weight_log", "解析数据异常,串口号:" + port + "  错误信息: " + ex.ToString() + "字符串为: " + aa, EFlag.error);
                            // myEvent(" ", flag);
                        }
                    }
                }
            }
         
        }  //读数据
        public int clear() //清零
        {
            int a = 0;
            byte[] b = new byte[1];
            b[0] = (byte)(0x5a);
            try
            {
                a = pComm.sio_write(port, ref b[0], b.Length);
                if (a < 0)
                {
                    LogClass.WriteLog("weight_log", "称重仪表发清零命令返回值" + port + "  " + a, EFlag.error);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                LogClass.WriteLog("weight_log", "称重仪表发清零命令异常" + port + "  " + ex.ToString(), EFlag.error);
            }
            return a;
        }
        public void Weight_Start() //调用线程
        {

            t = new Thread(OpenPort);
            thread_end = true;
            t.Start();
        }
        public void bend()
        {
            thread_end = false;
            t.Abort();
            ClosePort();

        }
    }
}
