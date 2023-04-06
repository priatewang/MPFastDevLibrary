using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PakeageTest
{


    public interface IUserDAL : IBaseDAL<MyUser>
    {

    }


    public class UserDAL : BaseDAL<MyUser>, IUserDAL
    {

    }


    public interface IMyClassDAL : IBaseDAL<MyClass>
    {

    }


    public class MyClassDAL : BaseDAL<MyClass>, IMyClassDAL
    {

    }
}
