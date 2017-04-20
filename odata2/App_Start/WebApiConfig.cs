using odata2.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.Http;
using System.Web.OData.Formatter;
using odata2.Lib;
using System.Web.OData.Formatter.Serialization;

namespace odata2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder()
            {
                ModelAliasingEnabled = true
            };

            builder.EntitySet<Teacher>("Teachers"); 
            builder.EntitySet<Course>("Courses");
            builder.EntityType<Location>();
            builder.EntitySet<ExternalLocation>("ExternalLocationsNonGraph");          
                       
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

            var odataFormatters = ODataMediaTypeFormatters.Create(new CustomODataSerializerProvider(),
                                                                  new CustomODataDeserializerProvider());

            config.Formatters.InsertRange(0, odataFormatters);

        }
    }
}
