using AppData.Context;
using AppData.Data;
using AppData.Repository;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDataAccess, DataAccess>();
builder.Services.AddTransient<IDataRepository, DataRepository>();

builder.Services.AddTransient<ILoginDBAccess, LoginDBAccess>();
builder.Services.AddTransient<ILoginDBRepository, LoginDBRepository>();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddTransient<IUserCbsDBAccess, UserCbsDBAccess>();
builder.Services.AddScoped<IUserCbsRepository, UserCbsRepository>();

builder.Services.AddCors();

builder.Services.AddHangfire((sp, config) =>
{
    var conString = sp.GetRequiredService<IConfiguration>().GetConnectionString("LoginDBConnectionString");
    config.UseSqlServerStorage(conString);
});
builder.Services.AddHangfireServer();
//builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
