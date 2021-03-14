using System;
using System.IO;

namespace HuntStat
{
    public static class PerkFileGenerator
    {
        /// <summary>
        /// В деректории .exe генерирует лист с перками вида (цена перка) (имя перка).
        /// </summary>
        public static void GeneratePerkList()
        {
            var sourse = GenerateSourseArray();

            GeneratePerkListFromSourse(sourse);
        }

        /// <summary>
        /// Собирает все строки с файла perks.txt, возвращает массив с ними.
        /// </summary>
        /// <returns>Массив строк, где каждому перку отведена своя строка.</returns>
        private static string[] GenerateSourseArray()
        {
            var sourseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"perks.txt");

            if (!File.Exists(sourseFilePath))
                throw new Exception("В папке с .exe нет файла-источника для перков.");

            return File.ReadAllLines(sourseFilePath);
        }

        /// <summary>
        /// Создает файл result.txt, в котором каждому перку отведена строка.
        /// </summary>
        /// <param name="sourse">Массив строк из файла sourse.txt</param>
        /// <returns>Если все пройдет хорошо, возвращается true.</returns>
        private static void GeneratePerkListFromSourse(string[] sourse)
        {
            var resultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"result.txt");

            if (File.Exists(resultPath))
                File.Delete(resultPath);

            var sw = File.AppendText(resultPath);

            using (sw)
            {
                sw.Write("(Cost of perk) (Name of perk)\n");
                foreach (var line in sourse)
                {
                    var splitedLineArray = line.Split(new char[] { '\t' });
                    var resultString = splitedLineArray[0] + " " + splitedLineArray[2];
                    sw.WriteLine(resultString);
                }
            }

            Console.WriteLine("Perk info generated");
        }
    }
}
