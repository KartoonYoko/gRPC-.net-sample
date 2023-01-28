

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IUpdateHandler, UpdateHandler>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddDbContext<TelegramBotContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("TelegramBotDB"),
        new MySqlServerVersion(new Version(8, 0, 28)));
});

var app = builder.Build();

app.MapControllers();

app.Run();
