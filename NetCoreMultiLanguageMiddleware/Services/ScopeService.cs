namespace NetCoreMultiLanguageMiddleware.Services
{
  public class ScopeService : IService
  {
    public string Id { get; set; }

    public ScopeService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
