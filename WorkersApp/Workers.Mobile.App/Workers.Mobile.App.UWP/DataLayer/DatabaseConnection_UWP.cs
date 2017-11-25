[assembly: Xamarin.Forms.Dependency(typeof(Workers.DataLayer.DatabaseConnection_UWP))]
namespace Workers.DataLayer
{
    using SQLite;
    using System.IO;
    using Windows.Storage;

    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "WorkersDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);

            return new SQLiteConnection(path);
        }
    }
}
