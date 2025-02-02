namespace Common_Serilog_dotnet.Services
{
    public class DummyService(ILogger<DummyService> logger) : IDummyService
    {
        public void DoSomething()
        {
            logger.LogInformation("something is done");
            logger.LogCritical("oops");
        }
    }
}
