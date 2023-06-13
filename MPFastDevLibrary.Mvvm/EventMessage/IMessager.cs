using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Mvvm
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMessager
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="msgTag">消息标识</param>
        /// <param name="action">消息执行的动作</param>
        void Subscribe(string msgTag, Action<object> action);

        /// <summary>
        /// 推送消息，触发订阅
        /// </summary>
        /// <param name="msgTag">消息标识</param>
        /// <param name="message">消息内容</param>
        void Publish(string msgTag, object message);

    }
}
