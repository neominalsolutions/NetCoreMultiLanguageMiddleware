namespace NetCoreMultiLanguageMiddleware.Services
{
  public class TransientService:IService
  {
    public string Id { get; set; }

    public TransientService()
    {
      Id = Guid.NewGuid().ToString();
    }
  }
}
