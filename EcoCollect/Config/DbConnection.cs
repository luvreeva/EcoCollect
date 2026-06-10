using Npgsql;

namespace EcoCollect.Config
{
    public static class DbConnection
    {
        private static string connString = "Host=localhost;Port=5432;Username=postgres;Password=Reeva97;Database=db_ecocollect";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }
    }
}