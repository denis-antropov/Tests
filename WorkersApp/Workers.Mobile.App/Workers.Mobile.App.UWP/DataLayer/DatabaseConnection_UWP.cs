[assembly: Xamarin.Forms.Dependency(typeof(Workers.DataLayer.DatabaseConnection_UWP))]
namespace Workers.DataLayer
{
    using System.IO;
    using Windows.Storage;

    public class DatabaseConnection_UWP : IDbFilePathProvider
    {
        public string GetFilePath()
        {
            var dbName = "WorkersDb.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);

            return path;
        }
    }
}
