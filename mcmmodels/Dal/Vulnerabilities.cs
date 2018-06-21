using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace mcmmodels.Dal
{
  public class Vulnerabilities
  {
    public List<VulnerabilityDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<VulnerabilityDal>("select id, vulnerability AS name from Vulnerabilities;").AsList();
        return result;
      }
    }

    public VulnerabilityDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<VulnerabilityDal>("select id, vulnerability AS name from Vulnerabilities where id=@id;", id);
        return result.FirstOrDefault();
      }
    }

    public int Insert(VulnerabilityDal vulnerability)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("insert into Vulnerabilities (vulnerability) values (@name);", new { name = vulnerability.Name });
        var result = conn.Query<VulnerabilityDal>("select id, vulnerability AS name from Vulnerabilities where name=@name;", vulnerability.Name).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(VulnerabilityDal vulnerability)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("update Vulnerabilities set vulnerability=@name where id=@id;", new { id = vulnerability.Id, name = vulnerability.Name });
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from Vulnerabilities where id=@id;", id);
      }
    }
  }

  public class VulnerabilityDal
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
