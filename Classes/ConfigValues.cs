using System;
using System.IO;

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
        private static int forceScanTime = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/forceScanTime.txt")));
        private static int sampleCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/sampleCount.txt")));

        private static string nirAddress = File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirAddress.txt"));
        private static int nirPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/nirPort.txt")));
        private static int pcLocalPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/pcLocalPort.txt")));

        private static string sendMessageFormat = File.ReadAllText(Path.GetFullPath("Configurations/NIR/sendMessageFormat.txt"));
        private static string endMessageFormat = File.ReadAllText(Path.GetFullPath("Configurations/NIR/endMessageFormat.txt"));

        private static string dbAddress = File.ReadAllText(Path.GetFullPath("Configurations/DB Config/DbConnection.txt"));
        private static string wbAddress = File.ReadAllText(Path.GetFullPath("Configurations/DB Config/WeighBridgeDB.txt"));
        private static string defaultDbAddress = File.ReadAllText(Path.GetFullPath("Configurations/DB Config/DefaultDBConnection.txt"));

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
        public int ForceScanTime
        {
            get
            {
                return forceScanTime;
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

        public int PcPort
        {
            get
            {
                return pcLocalPort;
            }
        }

        public int SampleCount
        {
            get
            {
                return sampleCount;
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

        public string WbAdress
        {
            get
            {
                return wbAddress;
            }
        }

        public string DefaultConnection
        {
            get
            {
                return defaultDbAddress;
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

        public void ChangeLocalPort(string port)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/pcLocalPort.txt"), port);
                pcLocalPort = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/pcLocalPort.txt")));
            }
            catch (Exception)
            {

            }
        }

        public void ScannedSample(string count)
        {
            try
            {
                File.WriteAllText(Path.GetFullPath("Configurations/NIR/sampleCount.txt"), count);
                sampleCount = int.Parse(File.ReadAllText(Path.GetFullPath("Configurations/NIR/sampleCount.txt")));
            }
            catch (Exception)
            {

            }
        }
    }
}
