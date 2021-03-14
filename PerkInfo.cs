using System;
using System.IO;

namespace HuntStat
{
    public class PerkInfo
    {
        public int PerkCost;
        public string PerkName;

        public PerkInfo(int perkCost, string perkName)
        {
            PerkCost = perkCost;
            PerkName = perkName;
        }

        public  static PerkInfo[] GeneratePerkInfoArray()
        {
            var resultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"result.txt");
            var resultLines = File.ReadAllLines(resultPath);

            var resultArray = new PerkInfo[resultLines.Length - 1];
            for (var i = 1; i < resultLines.Length; i++)
            {
                var splitedLine = resultLines[i].Split(" ", 2);
                resultArray[i - 1] = new PerkInfo(int.Parse(splitedLine[0]), splitedLine[1]);
            }

            return resultArray;
        }
    }
}