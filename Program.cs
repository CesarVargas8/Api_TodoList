using System.Text.Json.Serialization;
using Api_TodoList.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddDbContext<PruebasBackendContext>();

builder.Services.AddCors(
    options =>
        options.AddDefaultPolicy(builder =>
        {
            builder
                .AllowAnyOrigin()
                .WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
                // .AllowAnyMethod()
                // .AllowAnyHeader()
                // .SetIsOriginAllowed(_ => true)
                // .AllowCredentials();
})
);
// Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();w

//Obtiene tareas por usuario
app.MapGet("/tasks/user/{idUser}", (PruebasBackendContext db, int idUser) => new ApiResponse( false, "Éxito", db.Tasks.Where(t => t.IdUser == idUser).ToList()));
// {
//     List<User>? users = null;
//     using ( var db = new PruebasBackendContext())
//     {
//         users = db.Users.Include(t=> t.Tasks).ToList();
//     }

//     return users == null ? Results.NotFound() : Results.Ok(users);
// });

// app.MapPost("/users", async (PruebasBackendContext db, User user) => {
//     db.Users.Add(user);
//     await db.SaveChangesAsync();
//     return Results.Created($"/users/{user.Id}", user);
// });

app.MapPost("/users/login", async (PruebasBackendContext db, LoginDTO user) => {
    var userLogin = await db.Users.Where(u => u.User1 == user.User && u.Password == user.Password).FirstOrDefaultAsync();
    if(userLogin is null) return new ApiResponse(true, "Usuario o contraseña incorrectos", null);
    return new ApiResponse(false, "Éxito", userLogin);
}); 

//Añadir tarea
app.MapPost("/tasks", async (PruebasBackendContext db, TaskDTO taskDTO) => {
    if(string.IsNullOrEmpty(taskDTO.Task)) return Results.BadRequest();
    db.Tasks.Add(new Api_TodoList.Models.Task(taskDTO));
    await db.SaveChangesAsync();
    return Results.Ok();
});

//Actualizar tarea
app.MapPut("/tasks/{id}", async (PruebasBackendContext db, int id, TaskDTO taskRequest) => {

    var task = await db.Tasks.FindAsync(id);

    if (task is null) return Results.NotFound();

    task.Task1 = taskRequest.Task;
    task.IdUser = taskRequest.IdUser;

    if (string.IsNullOrEmpty(task.Task1)) return Results.BadRequest();

    task.DateTask = DateTime.UtcNow;

    await db.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("/tasks/{id}", async (PruebasBackendContext db, int id)=>{
    var task = await db.Tasks.FindAsync(id);
    if(task is null) return Results.NotFound();

    db.Tasks.Remove(task);
    await db.SaveChangesAsync();
    return Results.Ok();
}); 

app.Run("http://localhost:5000");
