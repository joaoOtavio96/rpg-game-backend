using RPGGame.Config;
using RPGGame.Game;
using RPGGame.Game.Cameras;
using RPGGame.Game.Collisions;
using RPGGame.Game.Commands;
using RPGGame.HostedService;
using RPGGame.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHostedService<GameHostedService>();
builder.Services.AddSingleton<IGame, Game>();
builder.Services.AddSingleton<MainCommandQueue>();
builder.Services.AddSingleton<CollisionService>();
builder.Services.AddSingleton<CommandService>();
builder.Services.AddSingleton<CameraService>();
builder.Services.Configure<GameConfig>(builder.Configuration.GetSection("Game"));
builder.Services.AddCors(c => c.AddPolicy("allow", builder =>
{
    builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allow");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/game");

app.Run();
