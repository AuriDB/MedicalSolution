using AppLogic;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPatientManager, PatientManager>();
builder.Services.AddSingleton<IAppointmentManager, AppointmentManager>();

// Connector original (HttpClient)
builder.Services.AddSingleton<IRHConnector, RHConnector>();

// Connectors nuevos para la práctica
builder.Services.AddSingleton<IRHRestSharpConnector, RHRestSharpConnector>();
builder.Services.AddSingleton<IRHFlurConnector, RHFlurConnector>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DemoPolicy",
        policy =>
        {
            policy.AllowAnyOrigin(); //mypage.com, www.mypage.com, localhost:3000, etc
            policy.AllowAnyMethod(); //post, get, put, delete, etc
            policy.AllowAnyHeader(); //application/json, aplication/xml, etc
        });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else //para que levante la publicación en Azure, ya que el entorno de producción no se detecta correctamente
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
