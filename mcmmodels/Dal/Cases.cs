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
        var result = conn.Query<CaseDal>(_selectSql + " order by mcm_number desc").AsList();
        return result;
      }
    }

    public CaseDal Get(int id)
    {
      using (var conn = Database.GetConnection())
      {
        var result = conn.Query<CaseDal>(_selectSql + " where id=@id", new { id });
        return result.FirstOrDefault();
      }
    }

    public int Insert(CaseDal casedata, List<int> raceEthnicityList, List<int> vulnerabilitiesList, List<CaseLawEnforcementDal> lawEnforcementList)
    {
      var newId = -1;
      if (casedata.CountyId <= 0)
        casedata.CountyId = 177;
      if (casedata.SchoolId <= 0)
        casedata.SchoolId = 1;
      using (var conn = Database.GetConnection())
      {
        var exists = conn.Query<int>("select id from case_data where mcm_number=@number", new { number = casedata.MCMNumber }).FirstOrDefault();
        if (exists > 0)
          throw new InvalidOperationException($"MCM Number {casedata.MCMNumber} already exists in database");

        conn.Execute(_insertSql, casedata);
        var result = conn.Query<CaseDal>(_selectSql + " where mcm_number=@number;", new { number = casedata.MCMNumber }).FirstOrDefault();
        newId = result == null ? -1 : result.Id;
      }
      if (newId > -1)
      {
        SaveRaceEthnicity(newId, raceEthnicityList);
        SaveVulnerabilities(newId, vulnerabilitiesList);
        SaveCaseLawEnforcementList(newId, lawEnforcementList);
      }
      return newId;
    }

    public void Update(CaseDal casedata, List<int> raceEthnicityList, List<int> vulnerabilitiesList, List<CaseLawEnforcementDal> lawEnforcementList)
    {
      if (casedata.CountyId <= 0)
        casedata.CountyId = 177;
      if (casedata.SchoolId <= 0)
        casedata.SchoolId = 1;
      using (var conn = Database.GetConnection())
      {
        conn.Execute(_updateSql, casedata);
      }
      SaveRaceEthnicity(casedata.Id, raceEthnicityList);
      SaveVulnerabilities(casedata.Id, vulnerabilitiesList);
      SaveCaseLawEnforcementList(casedata.Id, lawEnforcementList);
    }

    public void Delete(int id)
    {
      DeleteRaceEthnicity(id);
      DeleteVulnerabilities(id);
      DeleteCaseLawEnforcementList(id);
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from case_data where id=@id;", new { id });
      }
    }

    public List<int> GetRaceEthnicity(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        IEnumerable<int> result = null;
        try
        {
          result = conn.Query<int>("select race_ethnicity_id from case_race_ethnicity where case_data_id=@caseId", new { caseId });
        }
        catch (Exception)
        {
          return null;
        }
        return result.ToList();
      }
    }

    public void SaveRaceEthnicity(int caseId, List<int> dataList)
    {
      using (var conn = Database.GetConnection())
      {
        DeleteRaceEthnicity(caseId);
        foreach (var item in dataList)
        {
          conn.Execute("insert into case_race_ethnicity (case_data_id, race_ethnicity_id) values (@caseId, @raceId)", new { caseId, raceId = item });
        }
      }
    }

    public void DeleteRaceEthnicity(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from case_race_ethnicity where case_data_id=@caseId;", new { caseId });
      }
    }

    public List<int> GetVulnerabilities(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        IEnumerable<int> result = null;
        try
        {
          result = conn.Query<int>("select vulnerabilities_id from case_vulnerabilities where case_data_id=@caseId", new { caseId });
        }
        catch (Exception)
        {
          return null;
        }
        return result.ToList();
      }
    }

    public void SaveVulnerabilities(int caseId, List<int> dataList)
    {
      using (var conn = Database.GetConnection())
      {
        DeleteVulnerabilities(caseId);
        foreach (var item in dataList)
        {
          conn.Execute("insert into case_vulnerabilities (case_data_id, vulnerabilities_id) values (@caseId, @itemId)", new { caseId, itemId = item });
        }
      }
    }

    public void DeleteVulnerabilities(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from case_vulnerabilities where case_data_id=@caseId;", new { caseId });
      }
    }

    public List<CaseLawEnforcementDal> GetCaseLawEnforcementList(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        IEnumerable<CaseLawEnforcementDal> result = null;
        try
        {
          result = conn.Query<CaseLawEnforcementDal>("select law_enforcement_id as AgencyId, jurisdictional_denial as Denial from case_lawenforcement_denial where case_data_id=@caseId", new { caseId });
        }
        catch (Exception)
        {
          return null;
        }
        return result.ToList();
      }
    }

    public void SaveCaseLawEnforcementList(int caseId, List<CaseLawEnforcementDal> dataList)
    {
      using (var conn = Database.GetConnection())
      {
        DeleteCaseLawEnforcementList(caseId);
        foreach (var item in dataList)
        {
          conn.Execute("insert into case_lawenforcement_denial (case_data_id, law_enforcement_id, jurisdictional_denial) values (@caseId, @itemId, @denial)", new { caseId, itemId = item.AgencyId, denial = item.Denial });
        }
      }
    }

    public void DeleteCaseLawEnforcementList(int caseId)
    {
      using (var conn = Database.GetConnection())
      {
        conn.Execute("delete from case_lawenforcement_denial where case_data_id=@caseId;", new { caseId });
      }
    }
  }

  public class CaseDal
  {
    public int Id { get; set; }
    public string MCMNumber { get; set; }
    public DateTime IntakeDate { get; set; }
    public DateTime LastSeen { get; set; }
    public DateTime ReportedMissing { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public int PeopleServed { get; set; }
    public string City { get; set; }
    public int CountyId { get; set; }
    public string State { get; set; }
    public string StartCaseType { get; set; }
    public string EndCaseType { get; set; }
    public string Disposition { get; set; }
    public DateTime CloseDate { get; set; }
    public string ReferralType { get; set; }
    public string CaseStatus {get; set; }
    public int SchoolId { get; set; }
  }

  public class CaseLawEnforcementDal
  {
    public int AgencyId { get; set; }
    public bool Denial { get; set; }
  }
}
