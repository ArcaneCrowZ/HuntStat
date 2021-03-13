using System;
using System.Collections.Generic;
using System.IO;

namespace HuntStat
{
    static class HuntStat
    {
        public static string LogDirectorPath = @".\Logs";
        public static PerkInfo[] PerksInfo = PerkInfo.GeneratePerkInfoArray();

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

        private static bool ExecuteCommand(string input)
        {
            if (input == "q") return true;

            if (input.Length == 1)
            {
                LairLog.AppendLogAboutLairs(input, LogDirectorPath);
            }

            if (input[0] == 'b')
            {
                ButcherLog.AppendLogAboutButhers(input, LogDirectorPath);
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
    }
}