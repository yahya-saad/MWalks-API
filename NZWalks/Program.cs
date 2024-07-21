using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails();


#region Register Dependencies

var defaultConnectionString = builder.Configuration.GetConnectionString("Default");
var authConnectionString = builder.Configuration.GetConnectionString("Auth");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(defaultConnectionString));
builder.Services.AddDbContext<AuthDbContext>(opt => opt.UseSqlServer(authConnectionString));

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<PaginationService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
#endregion

#region Identity Setup
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("MWalks")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureOptions<ConfigureIdentityOptions>();
#endregion

#region JWT
builder.Services.ConfigureOptions<JwtOptionsSetup>();
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings?.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseExceptionHandler();



app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "uploads")),
    RequestPath = "/uploads"
});
app.MapControllers();


app.Run();
