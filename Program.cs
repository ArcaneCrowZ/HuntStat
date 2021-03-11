using System;
using System.IO;

class Test
{
    public static string LogDirectorPath = @".\Logs";

    public static void Main()
    {
        InitializeDirectory();

        var input = string.Empty;
        while (true)
        {
            input = Console.ReadLine();
            if (input == "q") break;

            if (input.Length == 1)
            {
                AppendLogAboutLairs(input);
                continue;
            }

            if (input[0] == 'b')
            {
                AppendLogAboutButhers(input);
            }
        }
    }

    /// <summary>
    /// Содает папку Log в текущей папке проекта.
    /// После этого, выводит сообщение об этом на консоль.
    /// </summary>
    private static void InitializeDirectory()
    {
        string path = @".";

        var dirs = Directory.GetDirectories(path);

        if (!Directory.Exists(path + @"\Logs"))
        {
            Directory.CreateDirectory(path + @"\Logs");
            Console.WriteLine("Log directory created!");
        }
    }

    /// <summary>
    /// В случае отсутствия директориии Log по конкретному пути, создает ее. 
    /// После этого, сообщает об этом на консоль.
    /// </summary>
    /// <param name="path"></param>
    private static void Initialize(string path)
    {
        var dirs = Directory.GetDirectories(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Console.WriteLine("Log directory created!");
        }
    }

    private static void AppendTextToTestFile(string textToAppend)
    {
        var sw = File.AppendText(LogDirectorPath + @"\test.txt");

        using (sw)
        {
            sw.WriteLine(textToAppend);
        }
    }

    /// <summary>
    /// Пробует заапдейтить лог по спавну лееров боссов.
    /// Пишет все это в файл LairLog.txt. Если его нет - создает.
    /// </summary>
    /// <param name="input"></param>
    private static void AppendLogAboutLairs(string input)
    {
        var filePath = @"\LairLog.txt";
        if (!File.Exists(LogDirectorPath + filePath))
        {
            File.AppendAllText(LogDirectorPath + filePath, "RNB = right under boss, OTD = on the distanse\n\n");
        }

        var sw = File.AppendText(LogDirectorPath + filePath);
        using (sw) 
        {
            if ("rnb".Contains(input)) 
                sw.WriteLine("rnb");
            if ("otd".Contains(input))
                sw.WriteLine("otd");
        }
    }

    private static void AppendLogAboutButhers(string input)
    {
        var filePath = @"\ButherLog.txt";

        if (!File.Exists(LogDirectorPath + filePath))
        {
            File.AppendAllText(LogDirectorPath + filePath, "Sourse of damage, (perk name | empty)\n\n");
        }

        var sw = File.AppendText(LogDirectorPath + filePath);
        using (sw)
        {
            sw.WriteLine(input.Substring(2));
        }
    }
}
