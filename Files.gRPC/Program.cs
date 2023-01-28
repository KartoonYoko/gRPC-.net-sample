

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new GlobalOptions() {
    FileFolder = builder.Configuration.GetSection("FileFolder").Value,
});
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(opt => true); ;
    });
});
builder.Services.AddDbContext<FilesContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("FilesRepository"),
        new MySqlServerVersion(new Version(8, 0, 28)));
});
builder.Services.AddScoped<IFileService, Files.BLL.Services.FileService>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

Directory.CreateDirectory(builder.Configuration.GetSection("FileFolder").Value);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                     Path.Combine(builder.Environment.ContentRootPath, 
                     builder.Configuration.GetSection("FileFolder").Value)),
    ServeUnknownFileTypes = true,
    DefaultContentType = "image/png",

    RequestPath = "/StaticFiles",
    OnPrepareResponse = ctx => {
        // добавим заголовок для cors ответов
        //ctx.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        // для неавторизированных пользователей - redirect to /
        //if (!ctx.Context.User.Identity.IsAuthenticated)
        //{
        //    // Can redirect to any URL where you prefer.
        //    ctx.Context.Response.Redirect("/");
        //}
    }
});


app.UseMiddleware<GetHostMiddleware>();
app.MapGrpcReflectionService();
app.MapGrpcService<Files.gRPC.Services.v1.FileService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
