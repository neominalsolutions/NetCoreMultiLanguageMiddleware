namespace NetCoreMultiLanguageMiddleware.Services
{
  public class SingletonService:IService
  {
    public string Id { get; set; }

    public SingletonService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
