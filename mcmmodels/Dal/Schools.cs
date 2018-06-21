using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace mcmmodels.Dal
{
  public class Schools
  {
    public List<SchoolDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<SchoolDal>("select id, school_name AS name from Schools;").AsList();
        return result;
      }
    }

    public SchoolDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<SchoolDal>("select id, school_name AS name from Schools where id=@id;", new { id });
        return result.FirstOrDefault();
      }
    }

    public int Insert(SchoolDal school)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("insert into Schools (school_name) values (@name);", new { name = school.Name });
        var result = conn.Query<SchoolDal>("select id, school_name AS name from Schools where school_name=@name;", new { name = school.Name }).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(SchoolDal school)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("update Schools set school_name=@name where id=@id;", new { id = school.Id, name = school.Name });
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from Schools where id=@id;", new { id });
      }
    }
  }

  public class SchoolDal
  {
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
