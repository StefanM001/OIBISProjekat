using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IDS
{
    public class IDSLog
    {
        private static string storedHash = "";
        private static List<string> breachReports = new List<string>();

        public void LogAlarm(string alarm)
        {
            if (storedHash.Equals(""))
            {
                File.AppendAllText("Log.txt", alarm);
                storedHash = CalculateHash("Log.txt");
            }
            else
            {
                string calculatedHash = CalculateHash("Log.txt");
                if(calculatedHash == storedHash)
                {
                    File.AppendAllText("Log.txt", alarm);
                    storedHash = CalculateHash("Log.txt");
                }
                else
                {
                    breachReports.Add($"{DateTime.Now} | Integrity Breached");
                    storedHash = CalculateHash("Log.txt");
                }
            }
            
        }

        public string CalculateHash(string filePath)
        {
            using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        public string GetReports()
        {
            string ret = "";
            foreach(string s in breachReports)
            {
                ret += $"{s}\n";
            }

            breachReports.Clear();
            return ret;
        }
    }
}
