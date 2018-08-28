namespace DataValidation.Domains
{
    public class SqlConnectionInfo : IConnectionInfo
    {
        public SqlConnectionInfo(string connectionString) => ConnectionString = connectionString;

        public string ConnectionString { get; set; }
    }
}