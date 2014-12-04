using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using API;

namespace PostNo
{
    public class PostNumbers : API.Lists<Interface.IpostNo>
    {
        public PostNumbers()
            : base()
        {
        }
    }
}
