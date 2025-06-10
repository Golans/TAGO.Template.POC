using System.Text.Json;
using Tago.Extensions.AspNetCore;
using TAGO.Template.Api.Extensions;

BaseServicesBuilder.Default.WithJwtAuthorization(false);

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBaseApp();
builder.Services.AddBaseAppServices(builder.Configuration);


builder.Services.AddMsSqlDataAccess(builder.Configuration);
builder.Services.AddMongoDataAccess(builder.Configuration);
builder.Services.AddRestApi(builder.Configuration);

builder.Services.AddBusinessDi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
});

var app = builder.Build();

app.UseBaseApp_BFB();
app.UseBaseApp_BFF();

//must come after app.UseBaseApp*
app.UseMiddleware<BusinessExeptionConverterMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();