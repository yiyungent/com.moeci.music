using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.moeci.music.Code.StaticStore
{
    public static class InviteCodeStore
    {
        public static Queue<string> InviteCodeQueue { get; set; } = new Queue<string>();
    }
}
