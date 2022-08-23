namespace SteamChecker.Model
{
    public class User
    {
        public string SteamID { get; set; }
        public string UserURL { get; set; }
        public string AccountName { get; set; }
        public string PersonaName { get; set; }
        public string RememberPassword { get; set; }
        public string WantsOfflineMode { get; set; }
        public string SkipOfflineModeWarning { get; set; }
        public string AllowAutoLogin { get; set; }
        public string MostRecent { get; set; }
        public string Timestamp { get; set; }

        public override string ToString()
        {
            return $"{AccountName} {PersonaName} {SteamID}";
        }
    }
}
