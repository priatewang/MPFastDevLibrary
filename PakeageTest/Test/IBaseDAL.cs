using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PakeageTest
{
    public interface IBaseDAL<T> where T : class, new()
    {

        #region 增加

        /// <summary>
        /// 增加元素
        /// </summary>
        /// <param name="t"></param>
        /// <returns>插入元素自增列的值</returns>
        int Add(T t);

        /// <summary>
        /// 批量增加元素
        /// </summary>
        /// <param name="t"></param>
        /// <returns>影响行数</returns>
        int Add(List<T> t);

        #endregion

        #region 删除

        /// <summary>
        /// 删除某一元素
        /// </summary>
        /// <param name="t">对象</param>
        bool Delete(T t);

        /// <summary>
        /// 批量删除元素
        /// </summary>
        /// <param name="t">对象集合</param>
        bool Delete(List<T> t);

        /// <summary>
        /// 根据id，删除元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(object id);

        /// <summary>
        /// 根据id数组，批量删除元素
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteByIds(object[] ids);

        #endregion

        /// <summary>
        /// 更新元素
        /// </summary>
        /// <param name="t"></param>
        bool Update(T t);

        /// <summary>
        /// 更新元素集合
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(List<T> t);


        T Query(object id);


    }
}
