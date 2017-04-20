
using System;
using System.Web.OData;
using System.Web.OData.Formatter.Serialization;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using odata2.Models;
using System.Web.OData.Query;
using System.Reflection;

namespace odata2.Lib
{
    public class CourseSerializer : ODataEntityTypeSerializer
    {
        public CourseSerializer(ODataSerializerProvider serializerProvider) : base(serializerProvider)
        {
        }


        public override ODataProperty CreateStructuralProperty(IEdmStructuralProperty structuralProperty, EntityInstanceContext entityInstanceContext)
        {
            //if (entry.TypeName == "odata2.Models.ExternalLocation")
            //{
                if (structuralProperty.Name != "id")
            {
                return base.CreateStructuralProperty(structuralProperty, entityInstanceContext);
            }
            else
            {
                return null;
            }
        }

        public override ODataEntry CreateEntry(SelectExpandNode selectExpandNode, EntityInstanceContext entityInstanceContext)
        {
            ODataEntry entry = base.CreateEntry(selectExpandNode, entityInstanceContext);
            if (entry != null)
            {
                if (entry.TypeName == "odata2.Models.ExternalLocation")
                {
                    string id = string.Empty;
                    try
                    {
                        // SelectExpandBinder.SelectAll<foo>
                        object locationBinder = entityInstanceContext.EdmObject;
                        PropertyInfo instanceProperty = locationBinder.GetType().GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                        object rawLocation = instanceProperty.GetValue(locationBinder);
                        ExternalLocation extLocation = null;
                        if (rawLocation != null)
                        {
                            extLocation = rawLocation as ExternalLocation;
                            if (extLocation != null)
                            {
                                id = extLocation.Id;
                            }
                        }
                        else
                        {
                            extLocation = entityInstanceContext.EntityInstance as ExternalLocation;
                            if (extLocation != null)
                            {
                                id = extLocation.Id;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        entry.Id = new Uri(id);
                    }
                }
            }
            return entry;
        }
    }
}