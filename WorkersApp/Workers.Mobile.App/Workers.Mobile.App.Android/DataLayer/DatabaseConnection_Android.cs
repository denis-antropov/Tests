[assembly: Xamarin.Forms.Dependency(typeof(Workers.DataLayer.DatabaseConnection_Android))]
namespace Workers.DataLayer
{
    using System.IO;

    public class DatabaseConnection_Android : IDbFilePathProvider
    {
        public string GetFilePath()
        {
            var dbName = "WorkersDb.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return path;
        }
    }
}