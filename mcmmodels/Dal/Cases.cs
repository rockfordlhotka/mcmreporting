using System.Collections.Generic;
using Dapper;
using System.Linq;
using System;

namespace mcmmodels.Dal
{
  public class Cases
  {
    private string _selectSql =
      @"select id, mcm_number as mcmnumber, intake_date as intakedate, age, gender, last_seen as lastseen, reported_missing as reportedmissing,
        people_served as peopleserved, city, county as countyid, state, start_case_type as startcasetype, end_case_type as endcasetype, disposition,
        close_date as closedate, referral_type as referraltype, case_status as casestatus, school as schoolid from case_data";
    private string _insertSql =
      @"insert into case_data (mcm_number, intake_date, age, gender, last_seen, reported_missing,
        people_served, city, county, state, start_case_type, end_case_type, disposition,
        close_date, referral_type, case_status, school) values (@mcmnumber, @intakedate, @age, @gender, @lastseen, @reportedmissing,
        @peopleserved, @city, @countyid, @state, @startcasetype, @endcasetype, @disposition,
        @closedate, @referraltype, @casestatus, @schoolid)";
    private string _updateSql =
      @"update case_data set mcm_number=@mcmnumber, intake_date=@intakedate, age=@age, gender=@gender, last_seen=@lastseen, 
        reported_missing=@reportedmissing, people_served=@peopleserved, city=@city, county=@countyid, state=@state, 
        start_case_type=@startcasetype, end_case_type=@endcasetype, disposition=@disposition,
        close_date=@closedate, referral_type=@referraltype, case_status=@casestatus, school=@schoolid where id=@id";

    public List<CaseDal> Get()
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<CaseDal>(_selectSql).AsList();
        return result;
      }
    }

    public CaseDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<CaseDal>(_selectSql + " where id=@id", id);
        return result.FirstOrDefault();
      }
    }

    public int Insert(CaseDal casedata)
    {
      using (var conn = Database.GetConnection())
      {
        var exists = conn.Query<int>("select id from case_data where mcm_number=@number", casedata.MCMNumber).FirstOrDefault();
        if (exists > 0)
          throw new InvalidOperationException($"MCM Number {casedata.MCMNumber} already exists in database");

        conn.Execute(_insertSql, casedata);
        var result = conn.Query<CaseDal>(_selectSql + " where mcm_number=@number;", casedata.MCMNumber).FirstOrDefault();
        return result == null ? -1 : result.Id;
      }
    }

    public void Update(CaseDal casedata)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute(_updateSql, casedata);
      }
    }

    public void Delete(int id)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from case_data where id=@id;", id);
      }
    }
  }

  public class CaseDal
  {
    public int Id { get; set; }
    public string MCMNumber { get; set; }
    public DateTimeOffset IntakeDate { get; set; }
    public DateTimeOffset LastSeen { get; set; }
    public DateTimeOffset ReportedMissing { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public int PeopleServed { get; set; }
    public string City { get; set; }
    public int CountyId { get; set; }
    public string State { get; set; }
    public string StartCaseType { get; set; }
    public string EndCaseType { get; set; }
    public string Disposition { get; set; }
    public DateTimeOffset CloseDate { get; set; }
    public string ReferralType { get; set; }
    public string CaseStatus {get; set; }
    public int SchoolId { get; set; }
  }
}
