namespace Catty.Core.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication UsePipelineMiddleWares(this WebApplication app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}