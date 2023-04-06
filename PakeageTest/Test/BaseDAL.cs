using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakeageTest
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        public int Add(T t)
        {
            return 0;

        }

        public int Add(List<T> t)
        {
            return 0;
        }


        public bool Delete(T t)
        {
            return false;
        }

        public bool Delete(List<T> list)
        {
            return false;
        }


        public bool DeleteById(object id)
        {
            return false;
        }

        public bool DeleteByIds(object[] ids)
        {
            return false;
        }

        public bool Update(T t)
        {
            return false;
        }

        public bool Update(List<T> list)
        {
            return false;
        }

        public T Query(object id)
        {
            return null;
        }



    }
}
