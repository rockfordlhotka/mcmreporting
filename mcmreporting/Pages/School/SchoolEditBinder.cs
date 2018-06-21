using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace mcmreporting.Pages.School
{
  /// <summary>
  /// 
  /// </summary>
  public class CslaModelBinder : Csla.Server.ObjectFactory, IModelBinder
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bindingContext"></param>
    /// <returns></returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext == null)
      {
        throw new ArgumentNullException(nameof(bindingContext));
      }

      var result = Csla.Reflection.MethodCaller.CreateInstance(bindingContext.ModelType);
      if (result == null)
        return Task.CompletedTask;
      var properties = Csla.Core.FieldManager.PropertyInfoManager.GetRegisteredProperties(bindingContext.ModelType);
      foreach (var item in properties)
      {
        var index = $"{bindingContext.ModelName}.{item.Name}";
        try
        {
          var value = bindingContext.ActionContext.HttpContext.Request.Form[index].FirstOrDefault();
          if (!string.IsNullOrWhiteSpace(value))
          {
            if (item.Type.Equals(typeof(string)))
              LoadProperty(result, item, value);
            else
              LoadProperty(result, item, Csla.Utilities.CoerceValue(item.Type, value.GetType(), null, value));
          }
        }
        catch (Exception ex)
        {
          throw new Exception($"Could not map {index} to model", ex);
        }
      }

      bindingContext.Result = ModelBindingResult.Success(result);
      return Task.CompletedTask;
    }
  }

  public class CslaModelBinderProvider : IModelBinderProvider
  {
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
      if (typeof(Csla.IBusinessBase).IsAssignableFrom(context.Metadata.ModelType))
        return new CslaModelBinder();

      return null;
    }
  }
}
