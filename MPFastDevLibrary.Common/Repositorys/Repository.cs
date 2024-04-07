using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    public class Repository<T> : SimpleClient<T>
        where T : class, new()
    {
        public Repository(ISqlSugarClient db)
        {
            base.Context = db;
        }

        /// <summary>
        /// 保存（更新或插入）
        /// </summary>
        /// <param name="entity"></param>
        public void Save(T entity)
        {
            var db = Context.Storageable(entity).ToStorage();

            db.AsInsertable.ExecuteCommand(); //不存在插入
            db.AsUpdateable.ExecuteCommand(); //存在更新
        }
    }
}
