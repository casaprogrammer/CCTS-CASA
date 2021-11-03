using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cane_Tracking.Classes
{
    class ConfigValues
    {
        private static int tipperOneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/tipperOneMaxCount.txt")));
        private static int tipperTwoMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/tipperTwoMaxCount.txt")));
        private static int dumpAndPileMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/dumpAndPileMaxCount.txt")));
        private static int mainCaneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/mainCaneMaxCount.txt")));
        private static int knivesAndShredderMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/knivesAndShredderMaxCount.txt")));
        private static int nirWashingTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirWashingTime.txt")));
        private static int nirTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirTimerCount.txt")));

        private static string nirAddress = File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirAddress.txt"));
        private static int nirPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirPort.txt")));

        private static string sendMessageFormat = File.ReadAllText(Path.GetFullPath("Configurations/NIR/sendMessageFormat.txt"));
        private static string endMessageFormat = File.ReadAllText(Path.GetFullPath("Configurations/NIR/endMessageFormat.txt"));

        private static string dbAddress = File.ReadAllText(Path.GetFullPath("Configurations/DB Config/DbConnection.txt"));


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

        public string NirSendMessage
        {
            get
            {
                return sendMessageFormat;
            }
        }

        public string NirEndMessage
        {
            get
            {
                return endMessageFormat;
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
                File.WriteAllText(Path.GetFullPath("Configurations/Cane Prep/tipperOneMaxCount.txt"), count);
                tipperOneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/tipperOneMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeTipperTwoCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/Cane Prep/tipperTwoMaxCount.txt"), count);
                tipperTwoMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/tipperTwoMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeDumpAndPileCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/Cane Prep/dumpAndPileMaxCount.txt"), count);
                dumpAndPileMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/dumpAndPileMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeMainCaneCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/Cane Prep/mainCaneMaxCount.txt"), count);
                mainCaneMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/mainCaneMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeKnivesAndShredderCount(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/Cane Prep/knivesAndShredderMaxCount.txt"), count);
                knivesAndShredderMaxCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/Cane Prep/knivesAndShredderMaxCount.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeWashingTime(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/nirWashingTime.txt"), count);
                nirWashingTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirWashingTime.txt")));
            }
            catch (Exception)
            {

            }
        }
        public void ChangeNirTime(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/nirTimerCount.txt"), count);
                nirTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirTimerCount.txt")));
            }
            catch (Exception)
            {

            }
        }

        public void ChangeNirAddress(string address)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/nirAddress.txt"), address);
                nirAddress = File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirAddress.txt"));
            }
            catch (Exception)
            {

            }
        }

        public void ChangeNirPort(string port)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/nirPort.txt"), port);
                nirPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirPort.txt")));
            }
            catch (Exception)
            {

            }
        }
    }
}
