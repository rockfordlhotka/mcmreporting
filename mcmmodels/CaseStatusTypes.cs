using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class CaseStatusTypes : ReadOnlyListBase<CaseStatusTypes, string>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add("Open");
      Add("Closed");
      IsReadOnly = true;
    }
  }
}
