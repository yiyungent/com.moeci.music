using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.moeci.music.Code.Store
{
    public class InviteCodeStore
    {
        public Queue<string> InviteCodeQueue { get; set; } = new Queue<string>();
    }
}
