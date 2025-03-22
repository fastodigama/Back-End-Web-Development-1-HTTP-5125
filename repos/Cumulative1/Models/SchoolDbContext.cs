using MySql.Data.MySqlClient;
namespace Cumulative1.Models
{
    public class SchoolDbContext
    {
        private static string user { get { return "root"; } }
        private static string password { get { return "root"; } }
        private static string database { get { return "school"; } }
        private static string server { get { return "localhost"; } }
        private static string port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server =" + server + "; user =" + user
                    + "; database=" + database + ";port=" + port
                    + "; password=" + password + "; convert zero datetime=true";
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}