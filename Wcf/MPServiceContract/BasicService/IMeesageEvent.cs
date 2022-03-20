using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPServiceContract.BasicService
{
    public interface IMeesageEvent
    {
        event Action<string> StringMessageRecived;

        event Action<Message> MessageRecived;
    }
}
