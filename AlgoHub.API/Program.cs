using AlgoHub.API.MappingProfile;
using AlgoHub.API.Services;
using AlgoHub.BLL.Interfaces;
using AlgoHub.BLL.Services;
using AlgoHub.DAL;
using AlgoHub.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton(new AlgoHubDbContext(connectionString));

SymmetricSecurityKey jwtSecurityKey = new(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]!));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            //ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = false,
            //ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = jwtSecurityKey,
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddLogging(o => o.SetMinimumLevel(LogLevel.Debug));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AlgoHub", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSwaggerDocument();
builder.Services.AddAutoMapper(typeof(AlgoHubProfile));

builder.Services.AddScoped<IStorageService, S3StorageService>(s =>
{
    var aws = builder.Configuration.GetSection("AWS");
    return new(aws["BucketName"]!, aws["AccessKey"]!, aws["Secret"]!, aws["Region"]!);
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICompilerService, JDoodleService>();
builder.Services.AddScoped<ISolveService, SolveService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddSingleton<IAuthService, JwtAuthService>((provider) =>
    new JwtAuthService(builder.Configuration["Jwt:Key"]!, jwtSecurityKey));

builder.Services.AddHttpClient();

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
