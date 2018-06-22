using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace mcmmodels.Dal
{
  public class RaceEthnicity
  {
    public List<RaceEthnicityDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<RaceEthnicityDal>("select id, race_ethnicity AS Name from race_ethnicity;").AsList();
        return result;
      }
    }

    public RaceEthnicityDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<RaceEthnicityDal>("select id, race_ethnicity AS Name from race_ethnicity where id=@id;", new { id });
        return result.FirstOrDefault();
      }
    }

    public int Insert(RaceEthnicityDal race_ethnicity)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("insert into RaceEthnicity (race_ethnicity) values (@name);", new { name = race_ethnicity.Name });
        var result = conn.Query<RaceEthnicityDal>("select id, race_ethnicity AS Name from race_ethnicity where name=@name;", race_ethnicity.Name).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(RaceEthnicityDal race_ethnicity)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("update race_ethnicity set race_ethnicity=@name where id=@id;", new { id = race_ethnicity.Id, name = race_ethnicity.Name });
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from race_ethnicity where id=@id;", new { id });
      }
    }
  }

  public class RaceEthnicityDal
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
