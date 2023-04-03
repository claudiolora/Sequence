namespace proof
{
    class Program
    {
        static List<Int32> numberList = new List<Int32>();
 
        static void scale(Int32[] combination, Int32 index)
        {
            if (index < 0)
                return;
            combination[index]++;
            if ( (index > 0) && (combination[index] >= numberList.Count - index) )
            {
                scale(combination, index - 1);
                combination[index] = combination[index - 1] + 1;
            }
        }
 
        static List<Int32[]> calculateCombinations(Int32 n)
        {
            Int32[] combination = Enumerable.Range(0, n).ToArray();
            List<Int32[]> combinations = new List<Int32[]>();
 
            while (combination[0] <= numberList.Count - n)
            {
                if (combination.All(x => x < numberList.Count))
                    combinations.Add(combination.Clone() as Int32[]);
                scale(combination, n - 1);
            }
 
            return combinations;
        }
 
        static void Main2(string[] args)
        {
            numberList.AddRange(new Int32[] { 0, 1, 2, 3, 4 });
            
            var clist = calculateCombinations(3);
            foreach (Int32[] c in clist)
                Console.WriteLine(c.Aggregate("", (acc, cur) => acc + ' ' + numberList[cur]));
 
            //Console.ReadKey();
        }
    }
}