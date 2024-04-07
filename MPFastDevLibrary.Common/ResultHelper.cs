using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPFastDevLibrary.Common
{
    public class ResultHelper
    {
        public static Result<T> Success<T>(T data)
        {
            return new Result<T>
            {
                Res = true,
                Message = "成功",
                Data = data
            };
        }

        public static Result Success()
        {
            return new Result { Res = true, Message = "成功", };
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T> { Res = false, Message = message };
        }

        public static Result Fail(string message)
        {
            return new Result { Res = false, Message = message };
        }
    }
}
