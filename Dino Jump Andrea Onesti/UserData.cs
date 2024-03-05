using System;
using System.Collections.Generic;
using System.Text;

namespace Dino_Jump_Andrea_Onesti
{
    public class UserData
    {
        public string Username { get; set; }
        public int Points { get; set; }

        public UserData() { }
        public UserData(string iUsername, int iPoints)
        {
            Username = iUsername;
            Points = iPoints;
        }
    }
}
