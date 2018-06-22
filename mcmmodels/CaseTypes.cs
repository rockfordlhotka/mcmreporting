using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class CaseTypes : ReadOnlyListBase<CaseTypes, string>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add("Runaway");
      Add("Abduction by Family Member");
      Add("Abduction by Acquaintance");
      Add("Stranger Abduction");
      Add("LIM");
      IsReadOnly = true;
    }
  }
}
