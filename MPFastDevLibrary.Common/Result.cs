namespace MPFastDevLibrary.Common
{
    /// <summary>
    /// 通用方法返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Res { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    public class Result : Result<bool> { }
}
