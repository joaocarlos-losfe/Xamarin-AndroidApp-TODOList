using System;
using System.IO;
using Tasks.Data;
using Tasks.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasks
{
    public partial class App : Application
    {
        static LocalDatabase local_database;

        public static LocalDatabase Local_database
        {
            get
            {
                if (local_database == null)
                {
                    local_database = new LocalDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return local_database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
