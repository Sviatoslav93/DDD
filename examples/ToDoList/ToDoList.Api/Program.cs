using ToDoList.Api;
using ToDoList.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(b =>
{
    b.AllowAnyOrigin();
    b.AllowAnyHeader();
    b.AllowAnyMethod();
});

app.UseExceptionHandler();

app.UseSwagger()
    .UseSwaggerUI();

app.MapToDoListEndpoints();

app.Run();
