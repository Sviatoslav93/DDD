using ToDoList.Api;
using ToDoList.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger()
    .UseSwaggerUI();

app.MapToDoListEndpoints();

app.Run();
