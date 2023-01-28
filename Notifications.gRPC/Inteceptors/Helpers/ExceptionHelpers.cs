namespace Notifications.gRPC.Interceptors.Helpers;

public static class ExceptionHelpers
{

    public static RpcException Handle(this Exception exception)
    {
        Status status = new(StatusCode.Internal, "Error");
        return new RpcException(status, CreateTrailers(Guid.NewGuid(), exception));
    }

    /// <summary>
    /// Adding the correlation to Response Trailers
    /// </summary>
    /// <param name="correlationId"></param>
    /// <returns></returns>
    private static Metadata CreateTrailers(Guid correlationId, Exception exception)
    {
        var trailers = new Metadata();
        trailers.Add("CorrelationId", correlationId.ToString());
        trailers.Add("Exception", exception.Message);
        if (exception.InnerException != null) trailers.Add("InnerException", exception.InnerException.Message);
        return trailers;
    }
}
