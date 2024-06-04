namespace Common
{
public static class Constants
{
	public const string DatabaseName = "generator_test_task";
	public const string TableName = "generator_table";
	public const string ConnectionStringInitial = "Host=localhost; Port=5432; User Id=postgres; Password=1111";
	public const string ConnectionString = ConnectionStringInitial + "; Database=" + DatabaseName;
}
}
