using OnlineStoreAPI.Data;
using OnlineStoreAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Services;
using FluentValidation;
using OnlineStoreAPI.Validators;
using OnlineStoreAPI.Middleware;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Name="Authorization",
        Type=SecuritySchemeType.Http,
        Scheme="bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,
        Description="Enter your JWT token."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy",policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,

                        ValidIssuer=builder.Configuration["Jwt:Issuer"],
                        ValidAudience=builder.Configuration["Jwt:Audience"],

                        IssuerSigningKey=new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                builder.Configuration["Jwt:Key"]!
                            )
                        )
                    };
                });
builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IPaymentRepository,PaymentRepository>();
builder.Services.AddScoped<IReviewRepository,ReviewRepository>();
builder.Services.AddScoped<IProductImageRepository,ProductImageRepository>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IPaymentService,PaymentService>();
builder.Services.AddScoped<IReviewService,ReviewService>();
builder.Services.AddScoped<IProductImageService,ProductImageService>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IAuthService,AuthService>();

builder.Services.AddValidatorsFromAssemblyContaining<ReviewCreateDtoValidator>();

var app = builder.Build();

app.UseStaticFiles();

app.UseCors("FrontendPolicy");

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


