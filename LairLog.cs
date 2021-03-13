using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HuntStat
{
    class LairLog
    {
        /// <summary>
        /// Пробует заапдейтить лог по спавну лееров боссов.
        /// Пишет все это в файл LairLog.txt. Если его нет - создает.
        /// </summary>
        /// <param name="input"></param>
        public static void AppendLogAboutLairs(string input, string logDirectorPath)
        {
            var filePath = @"\LairLog.txt";
            if (!File.Exists(logDirectorPath + filePath))
            {
                File.AppendAllText(logDirectorPath + filePath, "RNB = right under boss, OTD = on the distanse\n\n");
            }

            var sw = File.AppendText(logDirectorPath + filePath);
            using (sw)
            {
                if ("rnb".Contains(input))
                    sw.WriteLine("rnb");
                if ("otd".Contains(input))
                    sw.WriteLine("otd");
            }
        }
    }
}
