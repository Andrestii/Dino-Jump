using System;
using System.Collections.Generic;
using System.Text;

namespace Dino_Jump_Andrea_Onesti
{
    public class Leaderboard
    {
        public List<UserData> userData = new List<UserData>();

        public Leaderboard() { }

        public void InsertData(string username, int scoreNum)
        {
            userData.Add(new UserData(username, scoreNum));
        }
    }
}
