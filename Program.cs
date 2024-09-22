using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//реЇструЇмо в DI
builder.Services.AddSingleton<InMemoryDatabase>(); // використовуЇмо синглтон щоб створити лише один екземпл€р класу за весь час житт€ додатку
builder.Services.AddScoped<IReportService, ReportService>();//використовуЇмо scoped щоб створювати екземпл€р дл€ кожного http запиту. –еЇструЇмо ≥нтерфейс з реал≥зац≥Їю

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

app.Run();
