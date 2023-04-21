namespace Sequence;

/// <summary>
/// http://www.carlovecchio.altervista.org/c----permutazioni-e-combinazioni.html
/// </summary>
public class Combinatory
{
    /// <summary>
    /// Combinazioni semplici (senza ripetizioni)
    /// C(n, k)
    /// </summary>
    /// <param name="n">Elementi</param>
    /// <param name="k">Lunghezza sequenza</param>
    /// <returns></returns>
    internal static List<int[]> SimpleCombinations(int n, int k)
    {
        // Gli n elementi sono i numeri da 0 a n-1.
        // In C(5, 3), prima combinazione è comb[0, 1, 2]; ultima combinazione è [2, 3, 4].

        List<int[]> ret = new List<int[]>();
        int[] comb = new int[k];     // Combinazione generica
        int[] combMax = new int[k];  // Ultima combinazione

        if (n == 0)
            return ret;
        if (k == 0)
            return ret;
        if (n < k)
            return ret;

        // Se l'elemento mobile è l'ultimo degli elementi
        // (è mobile di sicuro perché è l'ultimo, quindi può essere spostato)
        // Allora aumenta l'indice di 1.
        // Altrimenti aumenta l'indice di 1 e imposta tutti gli altri elementi negli indici consecutivi

        // Posizione iniziale
        for (int i = 0; i < k; i++)
        {
            comb[i] = i;
        }
        // È la prima soluzione
        ret.Add((int[])comb.Clone());

        // Calcolo dell'ultima soluzione
        for (int i = 0; i < k; i++)
        {
            combMax[i] = n - k + i;
        }

        while (true)
        {
            // C(5, 3)
            // 0: [0, 1, 2*]
            // 1: [0, 1, 3*]
            // 2: [0, 1*, 4]
            // 3: [0, 2, 3*]
            // 4: [0, 2*, 4]
            // 5: [0*, 3, 4]
            // 6: [1, 2, 3*]
            // 7: [1, 2*, 4]
            // 8: [1*, 3, 4]
            // 9: [2, 3, 4]


            // Nella combinazione attuale, si cerca l'indice incrementabile più alto.
            bool trovatoIncrementabile = false;
            int indiceIncrementabile = -1;
            for (int i = k - 1; i >= 0; i--)
            {
                // Se l'indice è l'ultimo, basta verificare che sia inferiore al massimo che può assumere
                if (i == k - 1)
                {
                    if (comb[i] < combMax[i])
                    {
                        // Trovato ed è l'ultimo elemento
                        trovatoIncrementabile = true;
                        indiceIncrementabile = i;
                        break;
                    }
                }
                // Altrimenti basta verificare che se fosse incrementato,
                // non sia uguale all'elemento successivo della combinazione
                else
                {
                    if (comb[i] < combMax[i] && comb[i] + 1 != comb[i + 1])
                    {
                        // Trovato ed è l'ultimo elemento
                        trovatoIncrementabile = true;
                        indiceIncrementabile = i;
                        break;
                    }
                }
            }

            // Se non trovato nessun valore incrementabile, l'algoritmo è finito
            if (!trovatoIncrementabile)
            {
                break;
            }
            // A questo punto, ho trovato un indice incrementabile
            // In ogni caso lo si incrementa
            comb[indiceIncrementabile]++;
            // Se non è l'ultimo, si posizionano gli altri indici nelle posizioni successive
            if (indiceIncrementabile != k - 1)
            {
                for (int i = indiceIncrementabile + 1; i < k; i++)
                {
                    comb[i] = comb[i - 1] + 1;
                }
            }
            // Nuova combinazione
            ret.Add((int[])comb.Clone());
        }

        return ret;
    }


    /// <summary>
    /// Combinazione con ripetizioni
    /// C'(n, k)
    /// </summary>
    /// <param name="n">Elementi</param>
    /// <param name="k">Lunghezza sequenza</param>
    internal static List<int[]> Combinations(int n, int k)
    {
        // Gli n elementi sono i numeri da 0 a n-1.
        // In C'(5, 3), prima combinazione è comb[0, 0, 0]; ultima combinazione è [4, 4, 4].
        //C(n,k) = n!/[k!*(n-k)!]
        //C'(n,k) = C(n+k-1, k) = (n+k-1)!/[k!(n-1)!]

        List<int[]> ret = new List<int[]>();
        int[] comb = new int[k];     // Combinazione generica
        int[] combMax = new int[k];  // Ultima combinazione

        if (n == 0)
            return ret;
        if (k == 0)
            return ret;
        if (n < k)
            return ret;

        // Se l'elemento mobile è l'ultimo degli elementi
        // (è mobile di sicuro perché è l'ultimo, quindi può essere spostato)
        // Allora aumenta l'indice di 1.
        // Altrimenti aumenta l'indice di 1 e imposta tutti gli altri elementi negli indici consecutivi

        // Posizione iniziale
        for (int i = 0; i < k; i++)
        {
            comb[i] = 0;
        }
        // È la prima soluzione
        ret.Add((int[])comb.Clone());

        // Calcolo dell'ultima soluzione
        for (int i = 0; i < k; i++)
        {
            combMax[i] = n - 1;
        }

        while (true)
        {

            // Nella combinazione attuale, si cerca l'indice incrementabile più alto.
            bool trovatoIncrementabile = false;
            int indiceIncrementabile = -1;
            for (int i = k - 1; i >= 0; i--)
            {
                // Se l'indice è l'ultimo, basta verificare che sia inferiore al massimo che può assumere
                if (i == k - 1)
                {
                    if (comb[i] < combMax[i])
                    {
                        // Trovato ed è l'ultimo elemento
                        trovatoIncrementabile = true;
                        indiceIncrementabile = i;
                        break;
                    }
                }
                // Altrimenti basta verificare che se fosse incrementato,
                // non sia uguale all'elemento successivo della combinazione
                else
                {
                    if (comb[i] <= combMax[i] && comb[i] < n - 1)
                    {
                        // Trovato ed è l'ultimo elemento
                        trovatoIncrementabile = true;
                        indiceIncrementabile = i;
                        break;
                    }
                }
            }

            // Se non trovato nessun valore incrementabile, l'algoritmo è finito
            if (!trovatoIncrementabile)
            {
                break;
            }
            // A questo punto, ho trovato un indice incrementabile
            // In ogni caso lo si incrementa
            comb[indiceIncrementabile]++;
            // Se non è l'ultimo, si posizionano gli altri indici nelle posizioni successive
            if (indiceIncrementabile != k - 1)
            {
                for (int i = indiceIncrementabile + 1; i < k; i++)
                {
                    comb[i] = comb[i - 1] ;
                }
            }
            // Nuova combinazione
            ret.Add((int[])comb.Clone());
        }

        return ret;
    }


    /// <summary>
    /// Permutazioni senza ripetizioni di tutti gli elementi
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static List<int[]> Permutations(int n)
    {
        int[] data = new int[n];
        for(int ii = 0; ii < n; ii++)
            data[ii] = ii;
        

        // Array di ritorno
        List<int[]> ret = new List<int[]>();

        // Si aggiunge la prima e si generano tutte le altre
        ret.Add((int[])data.Clone());
        if (data.Length <= 1)
            return ret;

        int numPermutazioni = Fattoriale(data.Length);
        for (int k = 0; k < numPermutazioni - 1; k++)
        {
            int i = data.Length - 1;
            while (data[i - 1] >= data[i])
                i--;

            int j = data.Length;
            while (data[j - 1] <= data[i - 1])
                j--;

            // Scambia data[i - 1] <--> data[j - 1]
            int temp = data[i - 1];
            data[i - 1] = data[j - 1];
            data[j - 1] = temp;

            i++;
            j = data.Length;
            while (i < j)
            {
                // Scambia data[i - 1] <--> data[j - 1]
                temp = data[i - 1];
                data[i - 1] = data[j - 1];
                data[j - 1] = temp;
                i++;
                j--;
            }

            ret.Add((int[])data.Clone());
        }

        return ret;
    }

    private static int Fattoriale(int number)
    {
        if (number < 0)
            return 0;
        else if (number <= 1)
            return 1;
        else
        {
            int prod = 1;
            for (int i = 2; i <= number; i++)
            {
                prod *= i;
            }
            return prod;
        }
    }

    public static int[] Permutation(int index, int elements)
    {
        int[] ret = new int[elements];
        int[] temp = new int[elements];
        int[] factoradic = Factoradic(index, elements);

        // Calcola la permutazione di indice 'index' (da 0 a max-1) in un insieme di 'elements' elementi,
        for (int i = 0; i < elements; i++)
            temp[i] = i;

        for (int i = 0; i < elements; i++)
        {
            ret[i] = temp[factoradic[i]];
            // Anziché rimuovere l'elemento, si spostano tutti quelli di indice successivo
            // (che non saranno più usati)
            for (int j = factoradic[i]; j < temp.Length - 1; j++)
            {
                temp[j] = temp[j + 1];
            }
        }

        return ret;
    }

    private static int[] Factoradic(int number, int elements)
    {
        int[] ret = new int[elements];

        for (int i = 1; i <= elements; ++i)
        {
            ret[elements - i] = number % i;
            number /= i;
        }

        return ret;
    }
    

}
