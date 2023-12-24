using System;
using System.Runtime.Serialization;

namespace Common
{
    public enum LevelOfSecurity { None, Information, Warning, Critical }

    [DataContract]
    public class Alarm
    {
        DateTime dateTime;
        string name;
        LevelOfSecurity level;

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
        public LevelOfSecurity Level
        {
            get { return level; }
            set { level = value; }
        }

        public Alarm(DateTime dateTime, string name, LevelOfSecurity level)
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
            return dateTime.ToString() + " | " + name + " | " + level.ToString() + "\n";
        }
    }
}
