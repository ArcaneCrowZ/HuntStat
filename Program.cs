using System;
using System.Collections.Generic;
using System.IO;

namespace HuntStat
{
    static class HuntStat
    {
        public static string LogDirectorPath = @".\Logs";
        public static PerkInfo[] PerksInfo = GeneratePerkInfoArray();

        public static void Main()
        {
            InitializeDirectory();
            PerkFileGenerator.GeneratePerkList();

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Length == 0) continue;

                var isBreak = ExecuteCommand(input);
                if (isBreak) break;
            }
        }

        private static PerkInfo[] GeneratePerkInfoArray()
        {
            var resultPath = @".\result.txt";
            var resultLines = File.ReadAllLines(resultPath);

            var resultArray = new PerkInfo[resultLines.Length - 1];
            for (var i = 1; i < resultLines.Length; i++)
            {
                var splitedLine = resultLines[i].Split(" ", 2);
                resultArray[i - 1] = new PerkInfo(int.Parse(splitedLine[0]), splitedLine[1]);
            }

            Console.WriteLine(resultArray.Length);

            return resultArray;
        }

        private static bool ExecuteCommand(string input)
        {
            if (input == "q") return true;

            if (input.Length == 1)
            {
                AppendLogAboutLairs(input);
            }

            if (input[0] == 'b')
            {
                AppendLogAboutButhers(input);
            }

            if (input == "help")
            {
                Console.WriteLine("b чтобы писать в файл бутчера, rnb - всратая катка, OTD - нормальная");
            }

            return false;
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

        /// <summary>
        /// Добавить строку в лог о бутчерах.
        /// </summary>
        /// <param name="input">Инпут. Первый символ - b, обозначающая обращение к этому логу. 
        /// После пробела - инпут.</param>
        private static void AppendLogAboutButhers(string input)
        {
            var filePath = @"\ButherLog.txt";

            if (!File.Exists(LogDirectorPath + filePath))
            {
                File.AppendAllText(LogDirectorPath + filePath, "Sourse of damage, (perk name | empty)\n\n");
            }

            var splitedInput = input.Substring(2).Split(" ");

            var nameToAdd = FindName(splitedInput[1]);
            if (nameToAdd == null)
            {
                Console.WriteLine("Не могу подобрать перк");
                return;
            }    

            var sw = File.AppendText(LogDirectorPath + filePath);
            using (sw)
            {
                sw.WriteLine(string.Format("{0} ({1})", splitedInput[0], nameToAdd));
            }
        }

        private static string FindName(string partofName)
        {
            var findedNames = FindIfPerkExists(partofName);
            if (findedNames.Length == 1)
                return findedNames[0];

            if (findedNames.Length == 0) return null;

            if (findedNames.Length > 1)
            {
                foreach (var name in findedNames)
                    Console.WriteLine(name);

                while (true)
                {
                    Console.WriteLine("Какой номер выбрать?");
                    var input = Console.ReadLine();
                    int inputedNumber;
                    if (!int.TryParse(input, out inputedNumber)) continue;

                    if (inputedNumber - 1 < findedNames.Length)
                        return findedNames[inputedNumber - 1];
                }
            }
            return "";
        }

        private static string[] FindIfPerkExists(string partOfPerkName)
        {
            var result = new List<string>();
            foreach (var perk in PerksInfo)
            {
                if (perk.PerkName.Contains(partOfPerkName))
                    result.Add(perk.PerkName);
            }

            return result.ToArray();
        }
    }
}