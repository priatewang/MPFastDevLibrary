using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Mvvm
{
    /// <summary>
    /// 事件聚合器-实现类
    /// </summary>
    public class Messager : IMessager
    {
        private Dictionary<string, List<Action<object>>> _subActions;

        private static readonly IMessager _Instance = new Messager();

        /// <summary>
        /// 默认单例
        /// </summary>
        public static IMessager Default
        {
            get { return _Instance; }
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="msgTag">消息标识</param>
        /// <param name="action">消息执行的动作</param>
        public void Subscribe(string msgTag, Action<object> action)
        {
            if (action == null)
            {
                return;
            }
            if (_subActions == null)
            {
                _subActions = new Dictionary<string, List<Action<object>>>();
            }
            _subActions.Add(msgTag, new List<Action<object>>());
            _subActions[msgTag].Add(action);
        }

        /// <summary>
        /// 推送消息，触发订阅
        /// </summary>
        /// <param name="msgTag">消息标识</param>
        /// <param name="message">消息内容</param>
        public void Publish(string msgTag, object message)
        {
            if (_subActions.TryGetValue(msgTag, out var actions))
            {
                foreach (var item in actions)
                {
                    item?.Invoke(message);
                }
            }
        }

        #region 测试方法

        void Test()
        {
            Subscribe("1", Act1);
        }

        void Act1(object o)
        {
            Console.WriteLine("Act1");
        }

        void act2(string s1)
        {
            Console.WriteLine("act2:" + s1);
        }
        #endregion
    }
}
