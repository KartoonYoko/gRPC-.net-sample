

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc(options => {
    options.MaxReceiveMessageSize = 6291456;
    options.MaxSendMessageSize = 6291456;

    options.CompressionProviders = new List<ICompressionProvider>() {
        new BrotliCompressionProvider(CompressionLevel.Optimal)
    };
    options.ResponseCompressionAlgorithm = "br"; // grpc accept-encoding, and must match the compression provider declared in CompressionProviders collection
    options.ResponseCompressionLevel = CompressionLevel.Optimal; // compression level used if not set on the provider

    options.Interceptors.Add<ExceptionInterceptor>();
});
builder.Services.AddGrpcReflection();
builder.Services.AddGrpcClient<Notifications.gRPC.Protos.v1.FileService.FileServiceClient>(options => {
    options.Address = new Uri(builder.Configuration.GetSection("FileServiceUrl").Value);
});
builder.Services.AddSingleton(new GlobalOptions());
builder.Services.AddDbContext<NotificationContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("TelegramBotDB"),
        new MySqlServerVersion(new Version(8, 0, 28)));
});
builder.Services.AddTransient<ITelegramAccountService, TelegramAccountService>();
builder.Services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();
builder.Services.AddTransient<TelegramClientCreator>();
builder.Services.AddTransient<NotificationContext>();

var app = builder.Build();


app.MapGrpcReflectionService();
app.UseMiddleware<GetHostMiddleware>();
app.MapGrpcService<AccountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
