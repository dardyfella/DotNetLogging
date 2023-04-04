using Serilog;




var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((host_builder_context, logger_configuration) => {
    logger_configuration.ReadFrom.Configuration(host_builder_context.Configuration);    
});


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();




if(builder.Environment.IsProduction()) {

}
else if(builder.Environment.IsDevelopment()) {
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}


var app = builder.Build();
app.UseSerilogRequestLogging();


if(app.Environment.IsProduction()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if(app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}



app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
