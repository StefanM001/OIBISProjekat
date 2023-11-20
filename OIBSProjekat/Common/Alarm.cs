using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    //Proveriti da li radi
    public enum CriticalLevel { INFORMATION, WARNING, CRITICAL }

    [DataContract]
    public class Alarm
    {
        DateTime dateTime;
        string name;
        CriticalLevel level;

        [DataMember]
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public CriticalLevel Level
        {
            get { return level; }
            set { level = value; }
        }

        public Alarm(DateTime dateTime, string name, CriticalLevel level)
        {
            this.dateTime = dateTime;
            this.name = name;
            this.level = level;
        }

        public Alarm()
        {
        }

        public override string ToString()
        {
            return dateTime.ToString() + " " + name + " " + level.ToString();
        }
    }
}
