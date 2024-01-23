namespace Catty.Core.Filters.Swagger
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var i in context.SchemaRepository.Schemas.Where(schema => schema.Value.AdditionalProperties is null))
                i.Value.AdditionalPropertiesAllowed = true;
        }
    }
}
