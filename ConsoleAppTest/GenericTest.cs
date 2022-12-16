using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    internal class GenericTest
    {
        public GenericTest()
        {
            IEnumerable<Father> people = new List<Son>();

            
            //协变
            //左边为父类，右边可以为子类
            Console.WriteLine("-----协变-----");
            IGenericX<Father> genericX = new GenericX<Father>();
            IGenericX<Father> genericX1 = new GenericX<Son>();
            // IGenericX<Son> genericX2 = new GenericX<Person>(); //错误
            genericX.Get();
            genericX1.Get();


            Console.WriteLine("-----逆变-----");
            //逆变
            //左边为子类，右边可以为父类
            IGenericN<Son> genericN = new GenericN<Son>();
            IGenericN<Son> genericN1 = new GenericN<Father>();
            // IGenericN<Person> genericN2 = new GenericN<Son>(); //错误
            genericN.Use(new Son());
            genericN1.Use(new Son());
        }

    }

    public class Father
    {
        public int ID { get; set; }

        public string Name { get; set; }

    }


    public class Son : Father
    {

    }

    public delegate void DelegateX<out T>();   
    public delegate T DelegateX1<out T>();   
    //public delegate T DelegateX2<out T>(T t);   //错误


    public interface IGenericX<out T>
    {
        // void Use(T t); //在协变中，T无法作为参数

        T Get(); //只能作为返回值
    }


    public class GenericX<T> : IGenericX<T>
    {
        public T Get()
        {
            Console.WriteLine(this.GetType().Name + "------" + typeof(T).Name);
            return default(T);
        }
    }


    public interface IGenericN<in T>
    {
        void Use(T t);  //T只能作为参数

        // T Get();  //逆变中，T无法作为返回值
    }

    public class GenericN<T> : IGenericN<T>
    {


        public void Use(T t)
        {
            Console.WriteLine(typeof(T).Name);

        }
    }
}
