using System;
using System.Net;
using System.Runtime.InteropServices;

namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 字节通信辅助类
    /// 可用于快速解析字节数组，转化成对应类型，包括了byte[]与class相互转换和大小端转换功能
    /// </summary>
    public class NetByteHelper
    {
        /// <summary>
        /// byte数组转class
        /// </summary>
        /// <typeparam name="T">类型（需要添加标签[StructLayout(LayoutKind.Sequential, Pack = 1)]对长度进行控制）</typeparam>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static T BytesToStruct<T>(byte[] bytes) where T : class, new()
        {
            T obj = new T();
            int size = Marshal.SizeOf(obj);
            // 如果结构体对象的字节数大于所给byte数组的长度，则返回空            
            if (size > bytes.Length)
            {
                return (default(T));
            }
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, structPtr, size);
            obj = (T)Marshal.PtrToStructure(structPtr, obj.GetType());
            Marshal.FreeHGlobal(structPtr);
            return (T)obj;
        }

        /// <summary>
        /// class转byte数组
        /// </summary>
        /// <typeparam name="T">类型（需要添加标签[StructLayout(LayoutKind.Sequential, Pack = 1)]对长度进行控制）</typeparam>
        /// <param name="structObj">传入的类对象</param>
        /// <returns></returns>
        public static byte[] StructToBytes<T>(T structObj) where T : class
        {
            // 获取结构体对象的字节数
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
            // 申请内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体内容拷贝到上一步申请的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            // 将数据拷贝到byte数组            
            Marshal.Copy(structPtr, bytes, 0, size);
            // 释放申请的内存            
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }
        //--------------------------------------------------------

        public static Type[] types = new Type[] { typeof(uint), typeof(UInt16), typeof(UInt64), typeof(UInt32) };

        /// <summary>
        /// 小端模式转大端模式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool LittleToBigEndian<T>(ref T obj) where T : class
        {
            var propertys = typeof(T).GetProperties();
            try
            {
                foreach (var item in propertys)
                {
                    if (item.PropertyType == typeof(uint) || item.PropertyType == typeof(int))
                    {
                        uint value = (uint)item.GetValue(obj);
                        item.SetValue(obj, (uint)IPAddress.HostToNetworkOrder((int)value));
                    }
                    else if (item.PropertyType == typeof(ushort) || item.PropertyType == typeof(short))
                    {
                        ushort value = (ushort)item.GetValue(obj);
                        item.SetValue(obj, (ushort)System.Net.IPAddress.HostToNetworkOrder((short)value));
                    }
                    else if (item.PropertyType == typeof(ulong) || item.PropertyType == typeof(long))
                    {
                        ulong value = (ulong)item.GetValue(obj);
                        item.SetValue(obj, (ulong)System.Net.IPAddress.HostToNetworkOrder((long)value));
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }

        /// <summary>
        /// 小端模式转大端模式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool BigToLittleEndian<T>(ref T obj) where T : class
        {
            var propertys = typeof(T).GetProperties();
            try
            {
                foreach (var item in propertys)
                {
                    if (item.PropertyType == typeof(uint) || item.PropertyType == typeof(int))
                    {
                        uint value = (uint)item.GetValue(obj);
                        item.SetValue(obj, (uint)System.Net.IPAddress.NetworkToHostOrder((int)value));
                    }
                    else if (item.PropertyType == typeof(ushort) || item.PropertyType == typeof(short))
                    {
                        ushort value = (ushort)item.GetValue(obj);
                        item.SetValue(obj, (ushort)System.Net.IPAddress.NetworkToHostOrder((short)value));
                    }
                    else if (item.PropertyType == typeof(ulong) || item.PropertyType == typeof(long))
                    {
                        ulong value = (ulong)item.GetValue(obj);
                        item.SetValue(obj, (ulong)System.Net.IPAddress.NetworkToHostOrder((long)value));
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class ByteTest
    {

    }
}
