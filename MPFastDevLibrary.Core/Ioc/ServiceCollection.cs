/*----------------------------------------------------------------
// 创建时间：2022/5/6 20:20:36
// 开发者： WQ
// 文件名： ServiceCollection
// CLR版本：4.0.30319.42000
// 命名空间：MPFastDevLibrary.Ioc
// 功能描述：
// 使用说明：
----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Ioc
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<int,ServiceDescriptor> _descriptors = new Dictionary<int, ServiceDescriptor>();

        public ServiceDescriptor this[int index]
        {
            get
            {
               return _descriptors[index];
            }

            set
            {
                _descriptors[index] = value;
            }
        }

        public int Count => _descriptors.Count;

        public bool IsReadOnly => false;

        public void Add(ServiceDescriptor item)
        {
            _descriptors.Add(item.ID, item);
        }

        public void Clear()
        {
            _descriptors.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.ContainsKey(item.ID);

        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.Values.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item.ID);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
