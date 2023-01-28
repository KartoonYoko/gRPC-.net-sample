

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHostedService<HostService>();
builder.Services.AddTransient<IUpdateRepository, UpdateRepository>();
builder.Services.AddTransient<TelegramClientCreator>();
builder.Services.AddTransient<NotificationContext>();
builder.Services.AddTransient<ITelegramAccountRepository, TelegramAccountRepository>();
builder.Services.AddDbContext<NotificationContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("TelegramBotDB"),
        new MySqlServerVersion(new Version(8, 0, 28)));
});

var app = builder.Build();

app.MapControllers();

app.Run();
