using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumSystem.Classes
{
    public static class Utiles
    {
        public static int QustionId;
        public static int TagId;
        public static string SplitBody(string input)
        {
            if(input.Length <= 100)
            {
                return input;
            }else
            {
                return input.Substring(0, 100) + "...";
            }
        }
       
    }
}