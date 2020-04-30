using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IncisionDKP
{
    class RetrieveDKP
    {
        private const string pathFile = @"D:\World of Warcraft\_classic_\WTF\Account\MIKE91\SavedVariables\MonolithDKP.lua";
        private static string MonoDKP { get; set; }

        public class Players
        {
            public string playerName { get; set; }
            public int totalDKP { get; set; }
        }

        public RetrieveDKP()
        {
            LoadFileContents();
        }

        private static void LoadFileContents()
        {
            if (!File.Exists(pathFile))
                throw new Exception("Could not find the Monolith DKP File.\r\nLocation: " + pathFile);

            string[] c = File.ReadAllLines(pathFile);
            int index = 0;
            bool logging = false;

            foreach(string line in c)
            {
                if (line.Contains("MonDKP_DKPTable"))
                    logging = true;
                else if (line.Contains("MonDKP_DKPHistory"))
                    logging = false;

                index++;

                if (!logging)
                    continue;


            }
        }

        private static void moo()
        {
            File.ReadAllLines("");
        }
    }
}
