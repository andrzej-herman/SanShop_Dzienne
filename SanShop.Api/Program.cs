using SanShop.Api.Services;
var builder = WebApplication.CreateBuilder(args);
string CorsOrigins = "_corsOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsOrigins,
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IProductService, ProductService>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors(CorsOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
