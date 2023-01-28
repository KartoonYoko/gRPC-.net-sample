
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcReflection();
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
builder.Services.AddDbContext<TelegramBotContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("TelegramBotDB"),
        new MySqlServerVersion(new Version(8, 0, 28)));
});
builder.Services.AddScoped<TelegramBotClientCreator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();


var app = builder.Build();

app.MapGrpcReflectionService();
app.MapGrpcService<TelegramBot.gRPC.Services.v1.AccountService>();
app.MapGrpcService<TelegramBot.gRPC.Services.v1.MessageService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
