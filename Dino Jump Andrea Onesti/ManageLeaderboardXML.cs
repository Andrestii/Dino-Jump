using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Dino_Jump_Andrea_Onesti
{
    class ManageLeaderboardXML
    {
        static string path = @"./../../../";
        static string fileName = "Leaderboard.xml";

        public static void LeaderboardWriter(List<UserData> ud)
        {
            try
            {
                ud.Sort((x, y) => y.Points.CompareTo(x.Points));
                XmlSerializer xmls = new XmlSerializer(typeof(List<UserData>));
                StreamWriter sw = new StreamWriter(path + fileName, false);
                xmls.Serialize(sw, ud);
                sw.Close();
            }
            catch (Exception e)
            {
                
            }
        }
        public static List<UserData> LeaderboardReader()
        {
            try
            {
                XmlSerializer xmls = new XmlSerializer(typeof(List<UserData>));
                StreamReader sr = new StreamReader(path + fileName);
                List<UserData> ud = (List<UserData>)xmls.Deserialize(sr);
                sr.Close();
                return ud;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
