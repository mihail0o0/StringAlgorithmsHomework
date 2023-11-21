using System.Diagnostics;

namespace AlgoritmiZaStringove;

class HelperFunctions
{
    private static HelperFunctions instance;
    private static HashSet<string> set;

    public HelperFunctions()
    {

    }

    public static HelperFunctions GetInstance()
    {
        instance ??= new HelperFunctions();
        return instance;
    }

    public void GenerisiText(int size, string path)
    {
        Random rnd = new();
        string text = "";

        for (int i = 0; i < size; i++)
        {
            text += set.ElementAt(rnd.Next(set.Count)) + " ";
        }

        try
        {
            using StreamWriter writer = new(path);

            writer.Write(text);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    public void GenerisiHexaText(int size, string path)
    {
        Random rnd = new();
        string text = "";
        int brojSlova;


        for (int i = 0; i < size; i++)
        {
            brojSlova = rnd.Next(2, 10);

            for (int j = 0; j < brojSlova; j++)
            {
                text += Convert.ToString(rnd.Next(0, 16), 16);
            }

            text += " ";
        }

        try
        {
            using StreamWriter writer = new(path);

            writer.Write(text);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    public string GenerisiHexaPattern(int size)
    {
        Random rnd = new();
        string text = "";
        int randomRazmak;


        randomRazmak = rnd.Next(2, 10);
        int br = 0;
        for (int i = 0; i < size; i++)
        {
            br++;
            if (br == randomRazmak)
            {
                text += " ";
                br = 0;
                randomRazmak = rnd.Next(2, 10);
                continue;

            }

            text += Convert.ToString(rnd.Next(0, 16), 16);
        }

        return text;
    }

    public string GenerisiPattern(int count)
    {
        Random rnd = new();
        string text = "";

        while (text.Length < count)
        {
            text += set.ElementAt(rnd.Next(set.Count)) + " ";
        }

        return (text.Length < count) ? text : text.Substring(0, count);
    }

    // stavalja sve reci iz dokumenta u jedan HashSet
    public void NapraviHashSet(string path)
    {
        set = new();

        try
        {
            string[] lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                string[] words = line.Split(new[] { " ", ",", ".", "?", "!", ";" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Replace("]", "").Replace("[", "").Replace("_", "").Replace(";", "").Replace("-", "").Replace("—", "").Replace(":", "");

                    words[i] = words[i].ToLower();

                    if (!set.Contains(words[i]))
                    {
                        set.Add(words[i]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
        }
    }
}