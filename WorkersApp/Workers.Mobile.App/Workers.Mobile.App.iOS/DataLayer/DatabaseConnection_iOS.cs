[assembly: Xamarin.Forms.Dependency(typeof(Workers.DataLayer.DatabaseConnection_iOS))]
namespace Workers.DataLayer
{
    using System;
    using SQLite;
    using System.IO;
    
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "WorkersDb.db3";
            var personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryPath, dbName);

            return new SQLiteConnection(path);
        }
    }
}