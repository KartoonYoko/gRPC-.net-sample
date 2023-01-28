

namespace Files.gRPC.Interceptors.Helpers
{
    public static class ExceptionHelpers
    {

        public static RpcException Handle(this Exception exception) {
            Status status = new(StatusCode.Internal, "Error");
            return new RpcException(status, CreateTrailers(Guid.NewGuid(), exception.Message));
        }

        /// <summary>
        /// Adding the correlation to Response Trailers
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        private static Metadata CreateTrailers(Guid correlationId, string message)
        {
            var trailers = new Metadata();
            trailers.Add("CorrelationId", correlationId.ToString());
            trailers.Add("ExceptionMessage", message);
            return trailers;
        }
    }
}
