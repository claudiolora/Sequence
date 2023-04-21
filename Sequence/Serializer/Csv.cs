namespace Sequence.Serializer
{
    public class Csv<T> : ISerializer<List<T[]>>
    {
        private string path;

        public Csv(string path)
        {
            this.path = path;
        }

        public void Write(List<T[]> res)
        {
            using (var file = File.CreateText(path))
            {
                for (int i = 0; i < res.Count; i++)
                {
                    for (int j = 0; j < res[i].Length; j++)
                    {
                        file.Write(res[i][j] + ";");
                    }
                    file.WriteLine("");
                }
            }
        }
    }
}
