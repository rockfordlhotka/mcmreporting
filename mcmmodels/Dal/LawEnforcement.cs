using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace mcmmodels.Dal
{
  public class LawEnforcement
  {
    public List<AgencyDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<AgencyDal>("select id, agency from law_enforcement;").AsList();
        return result;
      }
    }

    public AgencyDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<AgencyDal>("select id, agency from law_enforcement where id=@id;", id);
        return result.FirstOrDefault();
      }
    }

    public int Insert(AgencyDal agency)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("insert into LawEnforcement (agency) values (@name);", new { name = agency.Agency });
        var result = conn.Query<AgencyDal>("select id, agency from law_enforcement where name=@name;", agency.Agency).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(AgencyDal agency)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("update law_enforcement set agency=@name where id=@id;", new { id = agency.Id, name = agency.Agency });
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from law_enforcement where id=@id;", id);
      }
    }
  }

  public class AgencyDal
  {
    public int Id { get; set; }
    public string Agency { get; set; }
  }
}
