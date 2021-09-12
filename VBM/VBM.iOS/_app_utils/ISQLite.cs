using Foundation;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using VBM._app_utils;
using VBM.iOS._app_utils;
using Xamarin.Forms;

[assembly: Dependency(typeof(IOS_SQLite))]
namespace VBM.iOS._app_utils
{
    class IOS_SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "OurDBStore.sqlite";
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            string libraryPath = Path.Combine(dbPath, "..", "Library"); // Library folder  
            var path = Path.Combine(libraryPath, dbName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}