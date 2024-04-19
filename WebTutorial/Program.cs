using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI_Tutorial.Data;
using WebAPI_Tutorial.Model;
using WebTutorial.Dtos.CommentDto;
using WebTutorial.GenericRepository;
using WebTutorial.Mapper;
using WebTutorial.Model;
using WebTutorial.Repository.Comment;
using WebTutorial.Repository.Stock;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//DB
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebTutorial"));
});

//repository
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

//đăng ký vòng đời cho Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStockGenericRepository, StockGenericRepository>();

//AddSingleton được tạo ra một lần duy nhất và được tái sử dụng cho tất cả các yêu cầu.
//có nghĩa là một phiên bản duy nhất của dịch vụ sẽ được sử dụng cho toàn bộ ứng dụng
//services.AddSingleton<IMyService, MyService>();

//AddScoped được tạo ra mỗi lần yêu cầu trong một phạm vi (scope) được chỉ định mỗi lần bạn yêu cầu dịch vụ,
//một phiên bản mới của dịch vụ sẽ được tạo ra
//services.AddScoped<IMyService, MyService>();

//AddTransient được tạo ra mỗi khi được yêu cầu. Mỗi lần bạn yêu cầu dịch vụ,
//một phiên bản mới của dịch vụ sẽ được tạo ra, ngay cả khi có các yêu cầu trước đó đang sử dụng dịch vụ này
//services.AddTransient<IMyService, MyService>();

//autoMapper
//builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
//builder.Services.AddControllersWithViews();

//NewTonSoftJson
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});

//jwt
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;//số 
    options.Password.RequireNonAlphanumeric = true; //không ký tự đặc biệt
    options.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =//được sử dụng khi một yêu cầu cần được xác thực thành công.
    options.DefaultChallengeScheme = //được sử dụng khi xác thực không thành công
                                      //và cần chuyển hướng người dùng đến một trang hoặc endpoint khác để thực hiện xác thực.
    options.DefaultScheme =//được sử dụng cho các yêu cầu không được xác thực hoặc không yêu cầu xác thực.
    options.DefaultSignInScheme =//được sử dụng khi người dùng đăng nhập vào ứng dụng.

     //được sử dụng khi người dùng đăng xuất khỏi ứng dụng.
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(optons =>
{
    //chứa các tham số để xác thực và xác nhận token JWT.
    optons.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //true yêu cầu xác thực Issuer của token
        ValidIssuer = builder.Configuration["JWT:Issuer"],// Xác định Issuer hợp lệ.
        ValidateAudience = true,//true xác thực Audience của token.
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey= true,//để yêu cầu xác thực khóa chữ ký của Issuer.
        IssuerSigningKey = new SymmetricSecurityKey( //Xác định khóa chữ ký của Issuer
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
