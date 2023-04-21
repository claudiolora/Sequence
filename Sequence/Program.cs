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
            try
            {
                s.Run();
            }
            catch (Exception e)
            {
                Console.Write(e);
                Thread.Sleep(5000);
            }
        }

    }

}
