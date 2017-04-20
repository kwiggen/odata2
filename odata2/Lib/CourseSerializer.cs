
using System.Web.OData;
using System.Web.OData.Formatter.Serialization;
using Microsoft.OData.Core;
using odata2.Models;

namespace odata2.Lib
{
    public class CourseSerializer : ODataEntityTypeSerializer
    {
        public CourseSerializer(ODataSerializerProvider serializerProvider) : base(serializerProvider)
        {
        }

        public override ODataEntry CreateEntry(SelectExpandNode selectExpandNode, EntityInstanceContext entityInstanceContext)
        {
            ODataEntry entry = base.CreateEntry(selectExpandNode, entityInstanceContext);
            if (entry != null)
            {
                if (entry.TypeName == "odata2.Models.ExternalLocation")
                {
                    var props = entry.Properties;


                    foreach (var prop in props)
                    {
                        if (prop.Name == "id")
                            entry.InstanceAnnotations.Add(new ODataInstanceAnnotation("kw.id",
                                                                                      new ODataPrimitiveValue(prop.Value)));;

                        
                    }
                    

                    //ExternalLocation location = entityInstanceContext.EntityInstance as ExternalLocation;
                    //if (location != null)
                    //{
                    //entry.InstanceAnnotations.Add(new ODataInstanceAnnotation("org.northwind.search.score", 
                    //                                                          new ODataPrimitiveValue(.Id)));
                    // }
                }
            }

            return entry;
        }
    }
}