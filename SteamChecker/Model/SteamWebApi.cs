using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamChecker.Model
{
    public class Player
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string avatarhash { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
    }

    public class Response
    {
        public List<Player> players { get; set; }
    }
    public class Root
    {
        public Response response { get; set; }
    }

    public class SteamWebApi
    {
        public string ApiKey = " ";
        public Player GetInformation(string steamid)
        {

            Player player = null;
            try
            {
                string data_ = new WebClient().DownloadString(
                    $"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={ApiKey}&steamids={steamid}"
                    );

                Root r = JsonConvert.DeserializeObject<Root>(data_);
                player = r.response.players.First();
            }
            catch (Exception)
            {

                 
            }
            return player;
        }
    }
}
