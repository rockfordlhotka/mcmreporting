using System;
using System.Collections.Generic;
using System.Text;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class CaseLawEnforcementList : NameValueListBase<int, bool>
  {
    public CaseLawEnforcementList()
    {
      IsReadOnly = false;
    }
  }
}
