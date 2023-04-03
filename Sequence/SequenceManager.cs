using System;
using System.Collections.Generic;

namespace Sequence;

public class SequenceManager
{
    private SequenceInputModel model;

    public SequenceManager(SequenceInputModel model)
    {
        this.model = model;
    }

    public List<object[]> Permutations()
    {
        List<int[]> per = Combinatory.Permutations(model.Elements.Length);
        return Replace(per, model.Elements);
    }

    public List<object[]> Permutation()
    {
        int[] per = Combinatory.Permutation(model.Repetitions, model.Elements.Length);
        return Replace(new List<int[]> { per }, model.Elements);
    }

    public List<object[]> SimpleCombinations()
    {        
        List<int[]> index = Combinatory.SimpleCombinations(model.Elements.Length, model.Repetitions);
        return Replace(index, model.Elements);
    }    

    public List<object[]> Combinations()
    {        
        List<int[]> index = Combinatory.Combinations(model.Elements.Length, model.Repetitions);
        return Replace(index, model.Elements);
        //return index;
    }  

    public static List<T[]> Replace<T>(List<int[]> index, T[] values)
        {
            List<T[]> result = new List<T[]>();
            foreach (int[] row in index)
            {
                T[] seq = new T[row.Length];
                for (int i = 0; i < row.Length; i++)
                    seq[i] = values[row[i]];
                result.Add(seq);
            }

            return result;
        }



}
