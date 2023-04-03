using Newtonsoft.Json;

namespace Sequence;

public class SequenceIO
{

    string fileIn = "Sequence.json";
    string fileOut = "Result.csv";

    SequenceInputModel model;

    public SequenceIO()
    {

        model = new SequenceInputModel()
        {
            //typology = "P",
            Repetitions = 2,
            Elements = new object[] { "a", "b", "c", "d", "e" }
            //elements = new object[] { 1, 2, 3, 4, 5 } //,6,7,8,9,10,11,12
        };


    }

    public void LoadFile()
    {
        if (File.Exists(fileIn))
        {
            string json = File.ReadAllText(fileIn);
            try
            {
                if (json is null)
                    return;

                model = System.Text.Json.JsonSerializer.Deserialize<SequenceInputModel>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Read file {fileIn} error");
                Console.WriteLine(e);
            }
        }
        else
        {
            using (var file = File.CreateText(fileIn))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, model);
            }
        }
    }

    public void Run()
    {
        SequenceManager manager = new SequenceManager(model);

        //ok
        //Console.WriteLine("Combinations");
        var result = manager.Combinations();

        //ok
        //Console.WriteLine("SimpleCombinations");
        //var result = manager.SimpleCombinations();

        //ok
        //Console.WriteLine("Permutations");
        //var result = manager.Permutations();

        //bho???
        //Console.WriteLine("Permutation");
        //var result = manager.Permutation();


        if (File.Exists(fileOut))
        {
            Console.WriteLine($"File already exists {fileOut}, Override [Y/N]?");

            string key = Console.ReadLine();
            //Console.WriteLine(key);
            //Thread.Sleep(5000);

            if (!key.ToUpper().Equals("Y"))
                return;
        }

        //PrintCsv<object>(a);
        PrintCsv<object>(result);
    }

    private void PrintConsole<T>(List<T[]> res)
    {
        Console.WriteLine($"Length {res.Count}");
        // Risultato
        for (int i = 0; i < res.Count; i++)
        {
            for (int j = 0; j < res[i].Length; j++)
            {
                Console.Write(res[i][j] + " ");
            }
            Console.WriteLine("");
        }
    }

    private void PrintCsv<T>(List<T[]> res)
    {
        try
        {
            Console.WriteLine($"Length {res.Count}");

            using (var file = File.CreateText(fileOut))
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
        catch(Exception)
        {
            Console.WriteLine("File may be open");
            Thread.Sleep(5000);
        }
    }
}
