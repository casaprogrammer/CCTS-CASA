using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cane_Tracking.Classes
{
    class ConfigValues
    {
        private static int tipperOneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt")));
        private static int tipperTwoMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt")));
        private static int dumpAndPileMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt")));
        private static int mainCaneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt")));
        private static int knivesAndShredderMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt")));
        private static int nirWashingTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirWashingTime.txt")));
        private static int nirTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirTimerCount.txt")));

        private static string nirAddress = File.ReadAllText(Path.GetFullPath("Configurations/nirAddress.txt"));
        private static int nirPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirPort.txt")));

        private static string dbAddress = File.ReadAllText(Path.GetFullPath("Configurations/DbConnection.txt"));


        public int TipperOneMaxCount
        {
            get
            {
                return tipperOneMaxCount;
            }
        }

        public int TipperTwoMaxCount
        {
            get
            {
                return tipperTwoMaxCount;
            }
        }

        public int DumpAndPileMaxCount
        {
            get
            {
                return dumpAndPileMaxCount;
            }
        }

        public int MainCaneMaxCount
        {
            get
            {
                return mainCaneMaxCount;
            }
        }

        public int KnivesAndShredderMaxCount
        {
            get
            {
                return knivesAndShredderMaxCount;
            }
        }

        public int WashingTime
        {
            get
            {
                return nirWashingTime;
            }
        }

        public int NirTime
        {
            get
            {
                return nirTime;
            }
        }

        public string NirAddress
        {
            get
            {
                return nirAddress;
            }
        }

        public int NirPort
        {
            get
            {
                return nirPort;
            }
        }

        public string DbAddress
        {
            get
            {
                return dbAddress;
            }
        }

        public void ChangeTipperOneCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt"), count);
                tipperOneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeTipperTwoCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt"), count);
                tipperTwoMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeDumpAndPileCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt"), count);
                dumpAndPileMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeMainCaneCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt"), count);
                mainCaneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeKnivesAndShredderCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt"), count);
                knivesAndShredderMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeWashingTime(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/nirWashingTime.txt"), count);
                nirWashingTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirWashingTime.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeNirTime(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/nirTimerCount.txt"), count);
                nirTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirTimerCount.txt")));
            }
            catch (Exception)
            {

            }
        }

        public void ChangeNirAddress(string address)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/nirAddress.txt"), address);
                nirAddress = File.ReadAllText(Path.GetFullPath("Configurations/nirAddress.txt"));
            }
            catch (Exception)
            {

            }
        }

        public void ChangeNirPort(string port)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/nirPort.txt"), port);
                nirPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/nirPort.txt")));
            }
            catch (Exception)
            {

            }
        }
    }
}
