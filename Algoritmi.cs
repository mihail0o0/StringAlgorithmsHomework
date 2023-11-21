using System.Diagnostics;
using System.Dynamic;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Principal;

namespace AlgoritmiZaStringove;

class Algoritmi
{
    private static Algoritmi? instance;
    private static Dictionary<int, uint>? primes;

    public Algoritmi()
    {

    }

    public static Algoritmi GetInstance()
    {
        instance ??= new Algoritmi();
        return instance;
    }

    public void RabinKart(string readPath, string writePath, string pattern)
    {
        uint degree = 1;

        for (int i = 0; i < pattern.Length - 1; i++)
        {
            degree *= 127;
        }


        // preprocess
        int len = pattern.Length;
        int tacnaPoklapanja = 0;
        uint hashPattern = 0;
        uint hashString = 0;

        for (int i = 0; i < len; i++)
        {
            hashPattern = hashPattern * 127 + pattern[i];
        }

        // process
        try
        {
            using StreamReader streamReader = new(readPath);
            using StreamWriter streamWriter = new(writePath);

            int character;
            int elapsed = 0;
            string window = "";


            while ((character = streamReader.Read()) != -1)
            {
                elapsed++;
                // dok jos window nije popunjen
                if (elapsed <= len)
                {
                    window += (char)character;
                    hashString = hashString * 127 + (uint)character;
                    continue;
                }


                // poklapanje
                if (hashPattern == hashString)
                {
                    if (window == pattern)
                    {
                        streamWriter.WriteLine(window);
                        tacnaPoklapanja++;
                    }
                }


                uint toRemove = (uint)window[0] * degree;
                window = window[1..] + (char)character;

                hashString = ((hashString - toRemove) * 127) + (uint)character;
            }

            if (hashPattern == hashString)
            {
                if (window == pattern)
                {
                    tacnaPoklapanja++;
                    streamWriter.WriteLine(window);
                }
            }

            streamWriter.Write($"{tacnaPoklapanja} Poklapanja");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    public void LevenstainFromText(string readPath, string writePath, string pattern)
    {
        try
        {
            using StreamReader streamReader = new(readPath);
            using StreamWriter streamWriter = new(writePath);

            streamWriter.WriteLine($"Trazeni string: {pattern}");

            int len = pattern.Length;
            int elapsed = 0;
            int pribliznaPoklapanja = 0;
            int character;
            string window = "";

            while ((character = streamReader.Read()) != -1)
            {
                elapsed++;

                if (elapsed <= len)
                {
                    window += (char)character;
                    continue;
                }

                window = window[1..] + (char)character;

                if (LevensteinDistance(window, pattern) <= 3)
                {
                    pribliznaPoklapanja++;
                    streamWriter.WriteLine(window);
                }
            }

            if (LevensteinDistance(window, pattern) <= 3)
            {
                pribliznaPoklapanja++;
                streamWriter.WriteLine(window);
            }

            streamWriter.Write(pribliznaPoklapanja);

        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    public int LevensteinDistance(string from, string to)
    {
        int m = from.Length;
        int n = to.Length;

        int[,] distance = new int[m + 1, n + 1];

        // racunamo za prvi red i kolonu
        for (int i = 0; i <= m; i++)
        {
            distance[i, 0] = i;
        }

        for (int j = 0; j <= n; j++)
        {
            distance[0, j] = j;
        }

        // onda racunamo za ostatak tablice na osnovu ovoga
        for (int j = 1; j <= n; j++)
        {
            for (int i = 1; i <= m; i++)
            {
                int cost = (from[i - 1] == to[j - 1]) ? 0 : 1;
                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1,      // ubacivanje
                             distance[i, j - 1] + 1),     // brisanje
                    distance[i - 1, j - 1] + cost);    // zamena
            }
        }

        return distance[m, n];
    }
}