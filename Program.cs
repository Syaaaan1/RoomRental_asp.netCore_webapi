using RoomRentalTZ_1at.Data;
using RoomRentalTZ_1at.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//�������� � DI
builder.Services.AddSingleton<InMemoryDatabase>(); // ������������� �������� ��� �������� ���� ���� ��������� ����� �� ���� ��� ����� �������
builder.Services.AddScoped<IReportService, ReportService>();//������������� scoped ��� ���������� ��������� ��� ������� http ������. �������� ��������� � ����������

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
