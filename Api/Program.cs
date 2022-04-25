using Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//scope: Sadece ilk session da yaratılır session boyunca bir daha yaratılmaz.
//session incelenecek;
builder.Services.AddSingleton<IClass1,Class1>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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