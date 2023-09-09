using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace WGU_App.Services
{
    internal class Settings
    {



        public Settings() { }
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }
}
