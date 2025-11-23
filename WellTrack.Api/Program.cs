using Microsoft.EntityFrameworkCore;
using WellTrack.Api.Data;


var builder = WebApplication.CreateBuilder(args);


// Configuração do banco de dados Azure SQL
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapControllers();


app.Run();
