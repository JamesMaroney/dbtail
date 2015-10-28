using System;
using System.Data.SqlClient;
using System.Threading;
using Jokedst.GetOpt;

namespace dbtail
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      string Server = "localhost";
      string DatabaseName = null;
      string ConnectionString = null;
      string Table = null;
      string Query = null;
      string Format = null;
      int PollInterval = 200;

      var opts = new GetOpt("Tail a database query just like you tail a file",
        new[]
        {
          new CommandLineOption('s', "server", "Database Server [localhost]", ParameterType.String, o => Server = (string)o), 
          new CommandLineOption('d', "database", "Database Name", ParameterType.String, o => DatabaseName = (string)o), 
          new CommandLineOption('c', "connectionstring", "Connection String (Overrides --server and --database)", ParameterType.String, o => ConnectionString = (string)o), 
          new CommandLineOption('t', "table", "Table Name to Query", ParameterType.String, o => Table = (string)o),
          new CommandLineOption('q', "query", "Custom SQL Query (Overrides --table)", ParameterType.String, o => Query = (string)o),
          new CommandLineOption('f', "format", "Custom String.Format output", ParameterType.String, o => Format = (string)o),
          new CommandLineOption('p', "pollinterval", "Poll Interval in milliseconds [200]", ParameterType.Integer, o => PollInterval = (int)o)
        });

      opts.ParseOptions(args);

      var valid = true;
      if (String.IsNullOrEmpty(ConnectionString) && String.IsNullOrEmpty(DatabaseName))
      {
        Console.Error.WriteLine("Database connection information required.");
        valid = false;
      }
      if (String.IsNullOrEmpty(Table) && String.IsNullOrEmpty(Query))
      {
        Console.Error.WriteLine("Table Name or Custom SQL Query required.");
        valid = false;
      }
      if (!valid)
      {
        opts.ShowUsage();
        return;
      }


      using (SqlConnection conn = new SqlConnection())
      {
        conn.ConnectionString = ConnectionString ?? String.Format("Server={0};Database={1};Trusted_Connection=true", Server, DatabaseName);
        conn.Open();

        
        var cmd = new SqlCommand( Query ?? String.Format("SELECT * FROM {0}", Table), conn);
        string lastRow = null;
        var triggered = true;
        while (true)
        {
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              var values = new object[reader.FieldCount];
              reader.GetValues(values);
              var currentRow = String.IsNullOrEmpty(Format) ? String.Join(" ", values) : String.Format(Format, values);

              if (!triggered)
              {
                if (lastRow == currentRow)
                {
                  triggered = true;
                }
                continue;
              }
              Console.WriteLine(currentRow);
              lastRow = currentRow;
            }
            triggered = false;
          }
          Thread.Sleep(TimeSpan.FromMilliseconds(PollInterval));
        }
      }
    }
  }
}