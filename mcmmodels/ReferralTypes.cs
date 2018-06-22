using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class ReferralTypes : ReadOnlyListBase<ReferralTypes, string>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add("Word of Mouth");
      Add("Facebook");
      Add("Website");
      Add("Referral by Law Enforcement");
      Add("Referral by County Worker");
      IsReadOnly = true;
    }
  }
}
