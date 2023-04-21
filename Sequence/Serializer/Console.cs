using System;

namespace Sequence.Serializer
{
    public class Console<T> : ISerializer<List<T[]>>
    {
        public void Write(List<T[]> obj)
        {
            // Risultato
            for (int i = 0; i < obj.Count; i++)
            {
                for (int j = 0; j < obj[i].Length; j++)
                {
                    Console.Write(obj[i][j] + " ");
                }
                Console.WriteLine("");
            }
        }
    }
}
