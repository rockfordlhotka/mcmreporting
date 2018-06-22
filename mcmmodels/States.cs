using System;
using Csla;

namespace mcmmodels
{
  [Serializable]
  public class States : ReadOnlyListBase<States, StateInfo>
  {
    private void DataPortal_Fetch()
    {
      IsReadOnly = false;
      Add(DataPortal.FetchChild<StateInfo>("AL", "Alabama"));
      Add(DataPortal.FetchChild<StateInfo>("AK", "Alaska"));
      Add(DataPortal.FetchChild<StateInfo>("AZ", "Arizona"));
      Add(DataPortal.FetchChild<StateInfo>("AR", "Arkansas"));
      Add(DataPortal.FetchChild<StateInfo>("CA", "California"));
      Add(DataPortal.FetchChild<StateInfo>("CO", "Colorado"));
      Add(DataPortal.FetchChild<StateInfo>("CT", "Connecticut"));
      Add(DataPortal.FetchChild<StateInfo>("DE", "Delaware"));
      Add(DataPortal.FetchChild<StateInfo>("DC", "District Of Columbia"));
      Add(DataPortal.FetchChild<StateInfo>("FL", "Florida"));
      Add(DataPortal.FetchChild<StateInfo>("GA", "Georgia"));
      Add(DataPortal.FetchChild<StateInfo>("HI", "Hawaii"));
      Add(DataPortal.FetchChild<StateInfo>("ID", "Idaho"));
      Add(DataPortal.FetchChild<StateInfo>("IL", "Illinois"));
      Add(DataPortal.FetchChild<StateInfo>("IN", "Indiana"));
      Add(DataPortal.FetchChild<StateInfo>("IA", "Iowa"));
      Add(DataPortal.FetchChild<StateInfo>("KS", "Kansas"));
      Add(DataPortal.FetchChild<StateInfo>("KY", "Kentucky"));
      Add(DataPortal.FetchChild<StateInfo>("LA", "Louisiana"));
      Add(DataPortal.FetchChild<StateInfo>("ME", "Maine"));
      Add(DataPortal.FetchChild<StateInfo>("MD", "Maryland"));
      Add(DataPortal.FetchChild<StateInfo>("MA", "Massachusetts"));
      Add(DataPortal.FetchChild<StateInfo>("MI", "Michigan"));
      Add(DataPortal.FetchChild<StateInfo>("MN", "Minnesota"));
      Add(DataPortal.FetchChild<StateInfo>("MS", "Mississippi"));
      Add(DataPortal.FetchChild<StateInfo>("MO", "Missouri"));
      Add(DataPortal.FetchChild<StateInfo>("MT", "Montana"));
      Add(DataPortal.FetchChild<StateInfo>("NE", "Nebraska"));
      Add(DataPortal.FetchChild<StateInfo>("NV", "Nevada"));
      Add(DataPortal.FetchChild<StateInfo>("NH", "New Hampshire"));
      Add(DataPortal.FetchChild<StateInfo>("NJ", "New Jersey"));
      Add(DataPortal.FetchChild<StateInfo>("NM", "New Mexico"));
      Add(DataPortal.FetchChild<StateInfo>("NY", "New York"));
      Add(DataPortal.FetchChild<StateInfo>("NC", "North Carolina"));
      Add(DataPortal.FetchChild<StateInfo>("ND", "North Dakota"));
      Add(DataPortal.FetchChild<StateInfo>("OH", "Ohio"));
      Add(DataPortal.FetchChild<StateInfo>("OK", "Oklahoma"));
      Add(DataPortal.FetchChild<StateInfo>("OR", "Oregon"));
      Add(DataPortal.FetchChild<StateInfo>("PA", "Pennsylvania"));
      Add(DataPortal.FetchChild<StateInfo>("RI", "Rhode Island"));
      Add(DataPortal.FetchChild<StateInfo>("SC", "South Carolina"));
      Add(DataPortal.FetchChild<StateInfo>("SD", "South Dakota"));
      Add(DataPortal.FetchChild<StateInfo>("TN", "Tennessee"));
      Add(DataPortal.FetchChild<StateInfo>("TX", "Texas"));
      Add(DataPortal.FetchChild<StateInfo>("UT", "Utah"));
      Add(DataPortal.FetchChild<StateInfo>("VT", "Vermont"));
      Add(DataPortal.FetchChild<StateInfo>("VA", "Virginia"));
      Add(DataPortal.FetchChild<StateInfo>("WA", "Washington"));
      Add(DataPortal.FetchChild<StateInfo>("WV", "West Virginia"));
      Add(DataPortal.FetchChild<StateInfo>("WI", "Wisconsin"));
      Add(DataPortal.FetchChild<StateInfo>("WY", "Wyoming"));
      IsReadOnly = true;
    }
  }

  [Serializable]
  public class StateInfo : ReadOnlyBase<StateInfo>
  {
    public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(c => c.Code);
    public string Code
    {
      get { return GetProperty(CodeProperty); }
      private set { LoadProperty(CodeProperty, value); }
    }

    public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
    public string Name
    {
      get { return GetProperty(NameProperty); }
      private set { LoadProperty(NameProperty, value); }
    }

    private void Child_Fetch(string code, string name)
    {
      Code = code;
      Name = name;
    }
  }
}
