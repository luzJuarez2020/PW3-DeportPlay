using AutoWrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins(
            "https://localhost:4200/"
            )
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
});

#endregion

#region SERVICES
//builder.Services.AddTransient<IPrestadorManagementService, PrestadorManagementService>();
#endregion


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

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions() { IsApiOnly = false });

app.UseAuthorization();

app.MapControllers();

app.Run();
