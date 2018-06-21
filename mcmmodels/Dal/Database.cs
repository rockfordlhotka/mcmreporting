using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace mcmmodels.Dal
{
  internal static class Database
  {
    private static readonly string _conn = (string)Csla.ApplicationContext.LocalContext["ConnectionString"];

    public static NpgsqlConnection GetConnection()
    {
      var result = new NpgsqlConnection(_conn);
      result.Open();
      return result;
    }
  }
}
