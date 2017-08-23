using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class pComm
    {
        public const int B50 = 0x0;
        public const int B75 = 0x1;
        public const int B110 = 0x2;
        public const int B134 = 0x3;
        public const int B150 = 0x4;
        public const int B300 = 0x5;
        public const int B600 = 0x6;
        public const int B1200 = 0x7;
        public const int B1800 = 0x8;
        public const int B2400 = 0x9;
        public const int B4800 = 0xA;
        public const int B7200 = 0xB;
        public const int B9600 = 0xC; //
        public const int B19200 = 0xD;
        public const int B38400 = 0xE;
        public const int B57600 = 0xF;
        public const int B115200 = 0x10;
        public const int B230400 = 0x11;
        public const int B460800 = 0x12;
        public const int B921600 = 0x13;

        // Mode setting
        public const int BIT_5 = 0x0;                //' Data bits define
        public const int BIT_6 = 0x1;
        public const int BIT_7 = 0x2;
        public const int BIT_8 = 0x3;

        public const int STOP_1 = 0x0;                //' Stop bits define
        public const int STOP_2 = 0x4;

        public const int P_EVEN = 0x24;               //' Parity define
        public const int P_ODD = 0x8;
        public const int P_SPC = 0x38;
        public const int P_MRK = 0x28;
        public const int P_NONE = 0x0;
        [DllImport("PCOMM.dll")]
        public static extern int sio_open(int port);
        [DllImport("PCOMM.dll")]
        public static extern int sio_close(int port);
        [DllImport("PCOMM.dll")]
        public static extern int sio_flush(int port, int func);
        [DllImport("PCOMM.dll")]
        public static extern int sio_ioctl(int port, int BaudRate, int mode);
        [DllImport("PCOMM.dll")]
        public static extern int sio_write(int port,ref byte buf, int len);
        [DllImport("PCOMM.dll")]
        public static extern int sio_read(int port, ref byte buf, int len);
        [DllImport("PCOMM.dll")]
        public static extern int sio_SetWriteTimeouts(int port, int totaltimeouts);
        [DllImport("PCOMM.dll")]
        public static extern int sio_SetReadTimeouts(int port, int totaltimeouts);
        [DllImport("PCOMM.dll")]
        public static extern int sio_GetWriteTimeouts(int port, ref int totaltimeouts);
        [DllImport("PCOMM.dll")]
        public static extern int sio_GetReadTimeouts(int port, ref int totaltimeouts);

    }
}
