using System;
using System.Collections.Generic;
using System.Linq;

namespace Sequence
{
    public class Program
    {


        public static void Main(string[] args)
        {
            SequenceIO s = new SequenceIO();
            s.LoadFile();
            s.Run();
        }

    }

}
