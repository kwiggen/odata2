using Microsoft.OData.Core.UriParser;
using odata2.Lib;
using odata2.Models;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter;

namespace odata2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder()
            {
                ModelAliasingEnabled = true
            };
            builder.EnableLowerCamelCase();
            builder.EntitySet<Teacher>("teachers"); 
            builder.EntitySet<Course>("courses");
            builder.EntitySet<File>("externalFilesNonGraph");
            builder.EntitySet<ExternalLocation>("externalLocationsNonGraph");

            config.EnableCaseInsensitive(true);
            config.EnableEnumPrefixFree(true);
            config.EnableUnqualifiedNameCall(true);
            config.SetUrlConventions(ODataUrlConventions.ODataSimplified); //allows for courses(id) and courses/id/ in the URI
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
