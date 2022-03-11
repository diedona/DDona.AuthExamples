using WebApi;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
var app = builder.Build();
startup.ConfigureApp(app, app.Environment);