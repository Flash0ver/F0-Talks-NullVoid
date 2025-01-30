using System.Text.Json.Serialization;
using F0.Talks.NullVoid.WebApiApp.Todo;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.RespectNullableAnnotations = true;
	options.SerializerOptions.RespectRequiredConstructorParameters = true;
	options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddSingleton<TodoStorage>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}
app.MapGet("/", string () => "Hello, Techorama 2024!");

app.MapGet("/todos/", GetAllTodos);
app.MapGet("/todos/{id}", GetTodo);
app.MapPost("/todos/", CreateTodo);
app.MapPut("/todos/{id}", UpdateTodo);
app.MapDelete("/todos/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos([FromServices] TodoStorage storage, CancellationToken cancellationToken)
{
	return TypedResults.Ok(await storage.ReadAsync(cancellationToken));
}

static async Task<IResult> GetTodo([FromRoute] long id, [FromServices] TodoStorage storage, CancellationToken cancellationToken)
{
	var value = await storage.ReadAsync(id, cancellationToken);
	return value is { } todo
		? TypedResults.Ok(todo)
		: TypedResults.NotFound();
}

static async Task<IResult> CreateTodo([FromBody] TodoData data, [FromServices] TodoStorage storage, CancellationToken cancellationToken)
{
	var task = storage.CreateAsync(data, cancellationToken);
	TodoItem item = await task;
	return TypedResults.Created($"/todo/{item.Id}", item);
}

static async Task<IResult> UpdateTodo([FromRoute] long id, [FromBody] TodoData data, [FromServices] TodoStorage storage, CancellationToken cancellationToken)
{
	bool updated = await storage.UpdateAsync(id, data, cancellationToken);
	return updated
		? TypedResults.NoContent()
		: TypedResults.NotFound();
}

static async Task<IResult> DeleteTodo([FromRoute] long id, [FromServices] TodoStorage storage, CancellationToken cancellationToken)
{
	TodoData? data = await storage.DeleteAsync(id, cancellationToken);
	return data is not null
		? TypedResults.NoContent()
		: TypedResults.NotFound();
}

[JsonSerializable(typeof(TodoData))]
[JsonSerializable(typeof(TodoItem))]
[JsonSerializable(typeof(TodoItem[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
