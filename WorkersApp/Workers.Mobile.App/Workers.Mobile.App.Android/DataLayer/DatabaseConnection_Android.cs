[assembly: Xamarin.Forms.Dependency(typeof(Workers.DataLayer.DatabaseConnection_Android))]
namespace Workers.DataLayer
{
    using System.IO;

    //[assembly:Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLite.SQLiteConnection DbConnection()
        {
            var dbName = "WorkersDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return new SQLite.SQLiteConnection(path);
        }
    }
}