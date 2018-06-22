using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace mcmmodels.Dal
{
  public class Counties
  {
    public List<CountyDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<CountyDal>("select id, county_name AS name from Counties").AsList();
        return result;
      }
    }

    public CountyDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<CountyDal>("select id, county_name AS name from Counties where id=@id;", new { id });
        return result.FirstOrDefault();
      }
    }

    public int Insert(CountyDal county)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("insert into Counties (county_name) values (@name);", new { name = county.Name });
        var result = conn.Query<CountyDal>("select id, county_name AS name from Counties where name=@name;", county.Name).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(CountyDal county)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("update Counties set county_name=@name where id=@id;", new { id = county.Id, name = county.Name });
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from Counties where id=@id;", new { id });
      }
    }
  }

  public class CountyDal
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
