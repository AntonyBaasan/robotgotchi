using FirebaseAdmin;
using Microsoft.Extensions.Options;
using Models.Database;
using Security.Auth;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


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


app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
