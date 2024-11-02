using AuthControl.Application.Configurations;
using AuthControl.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()  
                   .AllowAnyMethod()  
                   .AllowAnyHeader(); 
        });
});
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();


//extensions
builder.Services.AddJwtAuthentication(jwtSettings); 
builder.Services.AddCustomFluentValidation();                
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection")) ; 
builder.Services.AddSwaggerDocumentation();          
builder.Services.AddApplicationServices();   
builder.Services.AddControllers();

// Build the app
var app = builder.Build();

// Enable Exception Handling
var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.ConfigureExceptionHandler(logger);


// Configure middleware
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthControl API v1");
    c.DocumentTitle = "AuthControl API Documentation";
});

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
