using System;
using System.Collections.Generic;
using System.IO;

namespace HuntStat
{
    static class HuntStat
    {
        public static string LogDirectorPath;
        public static PerkInfo[] PerksInfo;

        public static void Main()
        {
            Initialize();

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
                Console.WriteLine("b чтобы писать в файл бутчера, rnb - всратая катка, otd - нормальная");
            }

            return false;
        }

        /// <summary>
        /// Содает папку Log в текущей папке проекта.
        /// После этого, выводит сообщение об этом на консоль.
        /// </summary>
        private static void Initialize()
        {
            PerkFileGenerator.GeneratePerkList();

            LogDirectorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Logs");

            if (!Directory.Exists(LogDirectorPath))
            {
                Directory.CreateDirectory(LogDirectorPath);
                Console.WriteLine("Log directory created!");
            }

            PerksInfo = PerkInfo.GeneratePerkInfoArray();
        }
    }
}