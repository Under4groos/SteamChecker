using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SteamChecker.Model
{
    public class Steam : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private ObservableCollection<User> users_ = new ObservableCollection<User>();

        public ObservableCollection<User> Users
        {
            set
            {
                users_ = value;
                OnPropertyChanged();
            }
            get => users_;



        }

        public Steam()
        {
            Parce();

        }

        public void Parce()
        {
            string path_ = @"";
            //try
            //{
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey steam = currentUserKey.OpenSubKey("Software\\Valve\\Steam");
                path_ = Path.Combine((string)steam.GetValue("SteamPath"), "config\\loginusers.vdf");

                if (!File.Exists(path_))
                    return;

                string data_ = File.ReadAllText(path_);
                string steamid_ = "";
                foreach (Match item in Regex.Matches(data_, "\\\"[0-9]+\\\"[\\s\\S]+?{[\\w\\W]+?}"))
                {
                    foreach (Match steamid in Regex.Matches(item.Value, "\"[0-9]+\""))
                    {

                        if (steamid.Value.Length > 10)
                        {
                            steamid_ = steamid.Value.Replace("\"", "");
                            break;
                        }
                    }
                    foreach (Match user in Regex.Matches(item.Value, "{[\\w\\W]+?}"))
                    {
                        User user_ = new User()
                        {
                            SteamID = steamid_,
                        };
                        int a = 0;
                        foreach (Match user_data in Regex.Matches(user.Value, "\\\"[\\w]+\\\"[ \\W]+?\\\"[\\w]+\\\""))
                        {
                            int v = 0;
                            string name_p = "";
                            string name_va = "";
                            foreach (Match value in Regex.Matches(user_data.Value.Trim(), @"[\w]+"))
                            {
                                if (v == 0)
                                    name_p = value.Value;
                                if (v == 1)
                                    name_va = value.Value;
                                v++;
                            }
                            switch (name_p)
                            {
                                case "AccountName":
                                    user_.AccountName = name_va;
                                    break;

                                case "PersonaName":
                                    user_.PersonaName = name_va;
                                    break;
                                case "Timestamp":
                                 //  TimeSpan result = TimeSpan.FromMilliseconds(long.Parse(name_va));

                                    user_.Timestamp = name_va;// result.ToString( );
                                    break;
                            }

                        }
                        Users.Add(user_);
                    }

                }
            //}
            //catch (Exception)
            //{
            //}
        }
    }
}

