var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<Setting>(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddMediatR(typeof(Program))
                .AddTransient<IParseExpression,ParseExpression>();

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
