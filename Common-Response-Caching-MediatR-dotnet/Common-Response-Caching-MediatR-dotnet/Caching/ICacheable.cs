namespace Common_Response_Caching_MediatR_dotnet.Caching
{
    public interface ICacheable
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        int SlidingExpirationInMinutes { get; }
        int AbsoluteExpirationInMinutes { get; }
    }
}
