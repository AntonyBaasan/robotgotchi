using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Database;
using Moralis;
using Security.Auth;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/robotgotchi-1";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/robotgotchi-1",
            ValidateAudience = true,
            ValidAudience = "robotgotchi-1",
            ValidateLifetime = true
        };
    });


builder.Services.Configure<RobotgotchiDatabaseSettings>(builder.Configuration.GetSection(nameof(RobotgotchiDatabaseSettings)));

var firebaseCredentialConfiguration = new RobotgotchiFirebaseCredential();
builder.Configuration.GetSection("firebase").Bind(firebaseCredentialConfiguration);

builder.Services.AddSingleton<FirebaseApp>(sp =>
{
    var googleCredential = CredentialUtility.GetGoogleCredential(firebaseCredentialConfiguration);
    return FirebaseApp.Create(new AppOptions() { Credential = googleCredential });
});

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.Configure<RobotgotchiDatabaseSettings>(
        builder.Configuration.GetSection(nameof(RobotgotchiDatabaseSettings)));

builder.Services.AddSingleton<IRobotgotchiDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<RobotgotchiDatabaseSettings>>().Value);

builder.Services.AddHttpClient<IMoralisService, MoralisService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Moralis:BaseUrl"));
    c.DefaultRequestHeaders.Add("x-api-key", builder.Configuration.GetValue<string>("Moralis:ApiKey"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
