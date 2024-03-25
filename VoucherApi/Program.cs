using Microsoft.EntityFrameworkCore;
using VoucherApi.Data;
using VoucherApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DB connection
var connectionString = builder.Configuration.GetConnectionString("VoucherDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); // Add console loggingZ
    builder.AddDebug();   // Add debug output window logging
    
});

//Inject Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//invoke the ApplyMigration method into pipeline
//ApplyMigration();

app.Run();


//Automatically apply all the pending migrations to the Database
//void ApplyMigration()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//        if (_db.Database.GetPendingMigrations().Count() > 0)
//        {
//            _db.Database.Migrate();
//        }
//    }
//}