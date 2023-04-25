using System.Linq.Expressions;
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
            //Console.WriteLine("Hello, World!");
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20001));
            //byte[] buff = Encoding.Default.GetBytes("Success");
            //socket.Send(buff);
            //byte[] recvBuff = new byte[1024];
            //int num = socket.Receive(recvBuff);
            //for (int i = 0; i < num; i++)
            //{
            //    Console.Write(recvBuff[i] + " ");
            //}
            //Console.WriteLine();
            //var res = NetByteHelper.BytesToStruct<MyStruct>(recvBuff);
            //Console.WriteLine($"start:{res.start};angles1:{res.angles[0]};angles8:{res.angles[7]};end:{res.end}");
            var a = Expression.Parameter(typeof(int), "a");
            var b = Expression.Parameter(typeof(int), "b");

            var sum = Expression.Add(a, b);

            var add = Expression.Lambda<Func<int, int, int>>(sum, a, b).Compile();

            Console.WriteLine(add(1, 2)); // 输出 3



            var students = new List<Student>
{
    new Student { Id = 1, Name = "张三", Age = 20, Score = 80 },
    new Student { Id = 2, Name = "李四", Age = 22, Score = 70 },
    new Student { Id = 3, Name = "王五", Age = 19, Score = 90 },
    new Student { Id = 4, Name = "赵六", Age = 21, Score = 50 }
};

            //Expression<Func<Student, bool>> exp1 =x => x.Score > 60;
            //Expression<Func<Student, bool>> exp2 = x => x.Ages > 18;
            // var expNew = Expression.AndAlso(exp2.Body, exp1.Body);

            var parameter = Expression.Parameter(typeof(Student), "x");
            Expression<Func<Student, bool>> exp1 = Expression
                .Lambda<Func<Student, bool>>(Expression.GreaterThan(Expression.Property(parameter, "Score"),
                Expression.Constant(60)), parameter);
            Expression<Func<Student, bool>> exp2 = Expression
                .Lambda<Func<Student, bool>>(Expression.GreaterThan(Expression.Property(parameter, "Age"),
                Expression.Constant(18)), parameter);
            var expNew = Expression.AndAlso(exp2.Body, exp1.Body);
            var expRes = Expression.Lambda<Func<Student, bool>>(expNew, parameter);

            var lamda = expRes.Compile();
            var res = students.Where(expRes.Compile());
            foreach (var item in res)
            {
                Console.WriteLine(item.ToString());

            }
            Console.ReadKey();

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

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Score { get; set; }

        public override string ToString()
        {
            return $"Name:{Name},Age:{Age},Score:{Score}";
        }

    }
}