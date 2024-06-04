using Common;
using Npgsql;

namespace Generator_TestTask;

public static class DatabaseGenerator
{
	public static void CreateDatabase()
	{
		var createDbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "create-db.sql");

		using var conn = new NpgsqlConnection(Constants.ConnectionStringInitial);
		conn.Open();

		var checkDatabaseQuery = $"SELECT 1 FROM pg_database WHERE datname = '{Constants.DatabaseName}'";

		using var cmd = new NpgsqlCommand(checkDatabaseQuery, conn);
		var databaseExists = cmd.ExecuteScalar();

		if (databaseExists is null)
		{
			var createDatabaseQuery = File.ReadAllText(createDbScriptPath);
			using var createCmd = new NpgsqlCommand(createDatabaseQuery, conn);

			createCmd.ExecuteNonQuery();
			Console.WriteLine($"База данных {Constants.DatabaseName} создана успешно.");
		}
		else
		{
			Console.WriteLine($"База данных {Constants.DatabaseName} уже существует.");
		}
	}

	public static void InitializeDatabase()
	{
		var initializeDbScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "initialize-db.sql");

		using var conn = new NpgsqlConnection(Constants.ConnectionString);
		conn.Open();

		var checkTableExistsQuery = $"SELECT EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = '{Constants.TableName}');";
		using var cmd = new NpgsqlCommand(checkTableExistsQuery, conn);
		var tableExists = (bool)cmd.ExecuteScalar()!;

		if (tableExists)
		{
			Console.WriteLine($"Таблица {Constants.TableName} уже существует.");
		}
		else
		{
			var initializeDatabaseQuery = File.ReadAllText(initializeDbScriptPath);
			using var createCmd = new NpgsqlCommand(initializeDatabaseQuery, conn);

			createCmd.ExecuteNonQuery();
			Console.WriteLine($"База данных {Constants.DatabaseName} успешно проинициализирована.");
		}
	}
}