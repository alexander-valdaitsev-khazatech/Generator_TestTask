using SqlKata;
using Npgsql;
using SqlKata.Compilers;
using System.Collections.Generic;
using Common;
using Microsoft.CodeAnalysis;
using System;

// Создать генератор кода, который создаст Class Library, в которой есть 3 класса:
// Class1, Class2, Class3. В каждом классе есть только поле public long Id.

// Также в этой Class Library есть 3 файла-маппинга .xml для NHibernate для каждого класса.

// Использовать SqlKata для выгрузки данных из postgres таблицы, Roslyn Generator для генерации

[Generator]
public sealed class SourceGenerator : IIncrementalGenerator
{
	private List<string> _classNames = [];

	public void RetrieveClassNamesFromDb()
	{
		using var conn = new NpgsqlConnection(Constants.ConnectionString);
		conn.Open();

		var compiler = new PostgresCompiler();
		var query = new Query(Constants.TableName);
		var sql = compiler.Compile(query).Sql;
		
		using var cmd = new NpgsqlCommand(sql, conn);
		using var reader = cmd.ExecuteReader();
		while (reader.Read())
		{
			_classNames.Add(reader.GetString(0));
		}
		
		foreach (var className in _classNames)
		{
			Console.WriteLine(className);
		}
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		throw new NotImplementedException();
	}
}