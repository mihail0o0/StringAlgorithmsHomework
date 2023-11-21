using System.Diagnostics;

namespace AlgoritmiZaStringove;
class Program
{
    static void Main(string[] args)
    {
        HelperFunctions.GetInstance().NapraviHashSet("RomeoAndJuliet.txt");

        int[] words = { 100, 1000, 10000, 100000 };
        int[] characters = { 5, 10, 20, 50 };

        Stopwatch sw;
        for (int i = 0; i < 4; i++)
        {
            sw = new();
            string wordsPath = $"RNGWords{words[i]}.txt";
            string wordsHexPath = $"RNGWordsHexa{words[i]}.txt";

            HelperFunctions.GetInstance().GenerisiText(words[i], wordsPath);
            HelperFunctions.GetInstance().GenerisiHexaText(words[i], wordsHexPath);

            for (int j = 0; j < 4; j++)
            {
                string rabinKartPath = $"RabinKart{words[i]}W{characters[j]}C.txt";
                string levenstainPath = $"Levenstain{words[i]}W{characters[j]}C.txt";

                string rabinKartHexaPath = $"RabinKartHexa{words[i]}W{characters[j]}C.txt";
                string levenstainHexaPath = $"LevenstainHexa{words[i]}W{characters[j]}C.txt";

                string pattern = HelperFunctions.GetInstance().GenerisiPattern(characters[j]);

                sw = new();
                sw.Start();
                Algoritmi.GetInstance().RabinKart(wordsPath, rabinKartPath, pattern);
                sw.Stop();
                System.Console.WriteLine($"Trazenje ${characters[j]} karaktera medju {words[i]} reci je trajalo {sw.ElapsedMilliseconds}ms");

                sw = new();
                sw.Start();
                Algoritmi.GetInstance().LevenstainFromText(wordsPath, levenstainPath, pattern);
                sw.Stop();
                System.Console.WriteLine($"Trazenje slicnih ${characters[j]} karaktera medju {words[i]} reci je trajalo {sw.ElapsedMilliseconds}ms");

                /* HEXA */

                pattern = HelperFunctions.GetInstance().GenerisiHexaPattern(characters[j]);
                sw = new();
                sw.Start();
                Algoritmi.GetInstance().RabinKart(wordsHexPath, rabinKartHexaPath, pattern);
                sw.Stop();
                System.Console.WriteLine($"Trazenje ${characters[j]} hexa karaktera medju {words[i]} reci je trajalo {sw.ElapsedMilliseconds}ms");

                sw = new();
                sw.Start();
                Algoritmi.GetInstance().LevenstainFromText(wordsHexPath, levenstainHexaPath, pattern);
                sw.Stop();
                System.Console.WriteLine($"Trazenje slicnih ${characters[j]} hexa karaktera medju {words[i]} reci je trajalo {sw.ElapsedMilliseconds}ms");
            }
        }
    }

}
