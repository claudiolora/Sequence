using System;

namespace Sequence.Serializer
{
    public class FactorySerialzer
    {
        public static ISerializer<List<object[]>> Get(string file = null)
        {
            string ext = Path.GetExtension(file);
            switch (ext)
            {
                case ".csv":
                    return new Csv<object>(file); 
                case ".xls":
                    return new Xls<object>(file); 
                default:
                    return new Console<object>(); 

            }
        }
    }
}
