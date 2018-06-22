using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class GenderTypes : ReadOnlyListBase<GenderTypes, string>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add("Female");
      Add("Male");
      Add("Non-binary");
      IsReadOnly = true;
    }
  }
}
