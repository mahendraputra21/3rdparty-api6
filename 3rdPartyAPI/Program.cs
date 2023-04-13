using _3rdPartyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration to the app
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var appSettings = builder.Configuration.GetSection("BaseUrlSettings");

// Add HttpClient instances to the service collection
builder.Services.AddHttpClient("TodosApi", c =>
{
    c.BaseAddress = new Uri(appSettings.GetValue<string>("TodosApi"));
});
builder.Services.AddHttpClient("PublicHolidaysApi", c =>
{
    c.BaseAddress = new Uri(appSettings.GetValue<string>("PublicHolidaysApi"));
});

// Add other services
builder.Services.AddTransient<ITodoApiService, TodoApiService>();
builder.Services.AddTransient<IHolidayApiService, HolidayApiService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
