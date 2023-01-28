﻿
namespace Notifications.gRPC.Interceptors;

public class ExceptionInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception e)
        {
            throw e.Handle();
        }
    }


    public override async Task<TResponse> ClientStreamingServerHandler
    <TRequest, TResponse>(
     IAsyncStreamReader<TRequest> requestStream,
     ServerCallContext context,
     ClientStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(requestStream, context);
        }
        catch (Exception e)
        {
            throw e.Handle();
        }
    }

    public override async Task ServerStreamingServerHandler<TRequest,
    TResponse>(
     TRequest request,
     IServerStreamWriter<TResponse> responseStream,
     ServerCallContext context,
     ServerStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            await continuation(request, responseStream, context);
        }
        catch (Exception e)
        {
            throw e.Handle();
        }
    }

    public override async Task DuplexStreamingServerHandler<TRequest,
    TResponse>(
     IAsyncStreamReader<TRequest> requestStream,
     IServerStreamWriter<TResponse> responseStream,
     ServerCallContext context,
     DuplexStreamingServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            await continuation(requestStream, responseStream, context);
        }
        catch (Exception e)
        {
            throw e.Handle();
        }
    }


}
