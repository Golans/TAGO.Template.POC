namespace TAGO.Template.MsSql.DataAccess.DbModel
{
    internal class DbOptionsBuilder
    {
        public string ConnectionString { get; set; }
        public TimeSpan? RequestTimeout { get; set; }
        public string Database { get; internal set; }
        public string Collection { get; internal set; }
    }
}
