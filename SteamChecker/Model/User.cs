using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamChecker.Model
{
    public class User
    {
        static string str_null = "<null>";
        public string SteamID { get; set; } = str_null;
        public string AccountName { get; set; } = str_null;
        public string PersonaName { get; set; } = str_null; 
        public string RememberPassword { get; set; } = str_null;
        public string WantsOfflineMode { get; set; } = str_null;
        public string SkipOfflineModeWarning { get; set; } = str_null;
        public string AllowAutoLogin { get; set; } = str_null;
        public string MostRecent { get; set; } = str_null;
        public string Timestamp { get; set; } = str_null;

        public override string ToString()
        {
            return $"${PersonaName} {SteamID} {AccountName}";
        }

    }
}
