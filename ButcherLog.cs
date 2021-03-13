using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HuntStat
{
    public static class ButcherLog
    {
        public static PerkInfo[] PerksInfo = PerkInfo.GeneratePerkInfoArray();

        public static void AppendLogAboutButhers(string input, string logDirectorPath)
        {
            var filePath = @"\ButherLog.txt";

            if (!File.Exists(logDirectorPath + filePath))
            {
                File.AppendAllText(logDirectorPath + filePath, "Sourse of damage, (perk name | empty)\n\n");
            }

            var splitedInput = input.Substring(2).Split(" ");

            var nameToAdd = FindName(splitedInput[1]);
            if (nameToAdd == null)
            {
                Console.WriteLine("Не могу подобрать перк");
                return;
            }

            var sw = File.AppendText(logDirectorPath + filePath);
            using (sw)
            {
                sw.WriteLine(string.Format("{0} ({1})", splitedInput[0], nameToAdd));
            }
        }

        private static string FindName(string partofName)
        {
            var findedNames = FindAllPossiblePerks(partofName);
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

        private static string[] FindAllPossiblePerks(string partOfPerkName)
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
