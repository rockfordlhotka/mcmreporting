using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class DispositionTypes : ReadOnlyListBase<DispositionTypes, string>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add("Missing Found Safe");
      Add("Missing Found Injured");
      Add("Missing Found Deceased");
      IsReadOnly = true;
    }
  }
}
