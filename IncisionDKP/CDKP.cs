using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Windows.Forms;

namespace IncisionDKP
{
    class CDKP
    { 
        public class Fight
        {
            public int id { get; set; }
            public int start_time { get; set; }
            public int end_time { get; set; }
            public int boss { get; set; }
            public string name { get; set; }
            public int size { get; set; }
            public int difficulty { get; set; }
            public bool kill { get; set; }
            public int partial { get; set; }
            public bool hasWorldBuffs { get; set; }
            public int bossPercentage { get; set; }
            public int fightPercentage { get; set; }
            public int lastPhaseForPercentageDisplay { get; set; }
            public List<object> maps { get; set; }
        }

        public class Fight2
        {
            public int id { get; set; }
        }

        public class Friendly
        {
            public string name { get; set; }
            public int id { get; set; }
            public int guid { get; set; }
            public string type { get; set; }
            public string server { get; set; }
            public string icon { get; set; }
            public List<Fight2> fights { get; set; }
        }

        public class Fight3
        {
            public int id { get; set; }
            public int instances { get; set; }
            public int groups { get; set; }
        }

        public class Enemy
        {
            public string name { get; set; }
            public int id { get; set; }
            public int guid { get; set; }
            public string type { get; set; }
            public string icon { get; set; }
            public List<Fight3> fights { get; set; }
        }

        public class Fight4
        {
            public int id { get; set; }
            public int instances { get; set; }
        }

        public class FriendlyPet
        {
            public string name { get; set; }
            public int id { get; set; }
            public int guid { get; set; }
            public string type { get; set; }
            public string icon { get; set; }
            public int petOwner { get; set; }
            public List<Fight4> fights { get; set; }
        }

        public class ExportedCharacter
        {
            public int id { get; set; }
            public string name { get; set; }
            public string server { get; set; }
            public string region { get; set; }
        }

        public class Root
        {
            public List<Fight> fights { get; set; }
            public string lang { get; set; }
            public List<Friendly> friendlies { get; set; }
            public List<Enemy> enemies { get; set; }
            public List<FriendlyPet> friendlyPets { get; set; }
            public List<object> enemyPets { get; set; }
            public List<object> phases { get; set; }
            public int logVersion { get; set; }
            public int gameVersion { get; set; }
            public string title { get; set; }
            public string owner { get; set; }
            public long start { get; set; }
            public long end { get; set; }
            public int zone { get; set; }
            public List<ExportedCharacter> exportedCharacters { get; set; }
        }

        private static Uri WEB_PATH = new Uri("https://classic.warcraftlogs.com/v1/report/fights/");
        public static Root report = new Root();
        public static bool reportReady = false;


        private static void status (string s, int type=0) {
            Form1.Status(s, Form1.infoBox, type);
        }


        public static void init(string id, int bonus=0) //grab shit all grabby like
        {
            status("Retrieving JSON from API");
            Uri reportUri = new Uri(WEB_PATH + id + "?api_key=" + IncisionDKP.Form1.API_KEY);

            using (WebClient wc = new WebClient())
            {
                try { wc.DownloadStringAsync(reportUri); }
                catch (Exception ex) { status("WEBCLIENT :: " + ex, 3); }

                wc.DownloadStringCompleted += (s, e) =>
                {
                    status("JSON Received. Attempting to deserialize..");
                    try { report = JsonConvert.DeserializeObject<Root>(e.Result); }
                    catch (Exception ex) { status("DESERIALIZATION :: " + ex, 3); }
                    reportReady = true;
                };
            }

        }
        //jfc ADHD stfu
        //cross ref boss encouter IDs with players encouter IDs

        public static Dictionary<string, List<int>> Friendlies = new Dictionary<string, List<int>>();//Everyone in the report, and the fight IDs they were in. //movme
        public static Dictionary<int, string> BossEcounters = new Dictionary<int, string>(); //move me

        public static void FindFriendlyNames() {
            foreach (var fl in report.friendlies)
                Friendlies.Add(fl.name, new List<int>());
        }


        public static void FindUserEncouters(string name)
        {
            foreach(var fl in report.friendlies)
                foreach(var k in fl.fights)
                    Friendlies[fl.name].Add(k.id);
        }


        public static void FindBossEncouters()
        {
            foreach (var en in report.fights)
                if (en.boss != 0)
                    BossEcounters.Add(en.id, en.name);
        }

        // if bossecounters[en.id] BOSS // + 
        /* boss id 6, 13
         * Bsk 1,2,3,4,5,6,7,8,9,10,11,12,13
         * Kruug 1,2,3,4,5,6,7,8,9,10,11,12,13
         * Sennie 1,2,3,4,5,6,7,8,9,10,11,12,13
         * 
         * 
         * 
         * 
         * 
         * /







        /*public CDKP(string id, int bonus=0)
        {
            status("Retrieving JSON from API");
            Uri reportUri = new Uri(WEB_PATH + id + "?api_key=" + IncisionDKP.Form1.API_KEY);

            using (WebClient wc = new WebClient())
            {
                try { wc.DownloadStringAsync(reportUri); }
                catch (Exception ex) { status("WEBCLIENT :: " + ex, 3); }

                wc.DownloadStringCompleted += (s, e) =>
                {
                    status("JSON Received. Attempting to deserialize..");
                    try { report = JsonConvert.DeserializeObject<Root>(e.Result); }
                    catch (Exception ex) { status("DESERIALIZATION :: " + ex, 3); }
                };
            }
        }*/


    }
}
