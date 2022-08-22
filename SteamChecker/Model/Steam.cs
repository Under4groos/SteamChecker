using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamChecker.Model.CodeBeautify;
using System;
using System.Collections.Generic;
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
            Users.Clear();
             RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey steam = currentUserKey.OpenSubKey("Software\\Valve\\Steam");
            path_ = Path.Combine((string)steam.GetValue("SteamPath"), "config\\loginusers.vdf");
            path_ = @"C:\Users\Maks\Downloads\loginusers.vdf";
            if (!File.Exists(path_))
                return;
            string data_ = File.ReadAllText(path_);

            data_ = data_.Replace("\"users\"", "");
            data_ = Regex.Replace(data_, "\"\t\t\"", "\":\"");
            data_ = Regex.Replace(data_, "\\\"[\\W]+?{", "\":\n{");
            data_ = Regex.Replace(data_, "}", "},");

            foreach (Match item in Regex.Matches(data_ , "[0-9]{13,40}"))
            {
                data_ = data_.Replace(item.Value, $"_{item}");
            }

            string strD_g_data_ = "";
            foreach (var item in data_.Split('\n'))
            {
                if (Regex.IsMatch(item, "\\\"[\\w]+?\\\":?\\\"[\\w\\W]+?\""))
                {
                    strD_g_data_ += item.Trim() + ",\n";
                    continue;
                }
                strD_g_data_ += item + "\n";
            }
            strD_g_data_ = Regex.Replace(strD_g_data_, "},[\\W]+?},", "}\n}");
            strD_g_data_ = Regex.Replace(strD_g_data_, "\\\",[\\W]+?}", "\"\n}");

            Dictionary<string, User> PairList  = JsonConvert.DeserializeObject<Dictionary<string, User>>(strD_g_data_ );
            foreach (var item in PairList.Keys)
            {
                PairList[item].SteamID = item.Replace("_", "");
            }
            foreach (var item in PairList.Values)
            {
                Users.Add(item);
            }
          
 
        }
    }
}

