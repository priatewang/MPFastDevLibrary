using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using MPFastDevLibrary.Common;

namespace ConsoleAppTest2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20001));
            byte[] buff = Encoding.Default.GetBytes("Success");
            socket.Send(buff);
            byte[] recvBuff = new byte[1024];
            int num = socket.Receive(recvBuff);
            for (int i = 0; i < num; i++)
            {
                Console.Write(recvBuff[i] + " ");
            }
            Console.WriteLine();
            var res = NetByteHelper.BytesToStruct<MyStruct>(recvBuff);
            Console.WriteLine($"start:{res.start};angles1:{res.angles[0]};angles8:{res.angles[7]};end:{res.end}");

        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        class MyStruct
        {
            public int start;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public double[] angles;

            public int end;
        }
    }
}