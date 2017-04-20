
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
            // Skip the 'id' property on ExternalLocations.
            if (StringUtilities.InvariantInsensitive(structuralProperty.Name, "id") == 0 &&
                StringUtilities.InvariantInsensitive(structuralProperty.DeclaringType.FullTypeName(), typeof(ExternalLocation).FullName) == 0 )
            {
                return null;
            }
            else
            {
                return base.CreateStructuralProperty(structuralProperty, entityInstanceContext);
            }
        }

        public override ODataEntry CreateEntry(SelectExpandNode selectExpandNode, EntityInstanceContext entityInstanceContext)
        {
            // Force the '@odata.id' property on ExternalLocations
            ODataEntry entry = base.CreateEntry(selectExpandNode, entityInstanceContext);
            if (entry != null)
            {
                if (StringUtilities.InvariantInsensitive(entry.TypeName, typeof(ExternalLocation).FullName) == 0)
                {
                    string id = string.Empty;
                    try
                    {
                        // All patterns I could test are an internal object with an 'Instance' property
                        object locationBinder = entityInstanceContext.EdmObject;
                        PropertyInfo instanceProperty = locationBinder.GetType().GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
                        ExternalLocation extLocation = null;
                        if (instanceProperty != null)
                        {
                            object rawLocation = instanceProperty.GetValue(locationBinder);
                            if (rawLocation != null)
                            {
                                extLocation = rawLocation as ExternalLocation;
                                if (extLocation != null)
                                {
                                    id = extLocation.Id;
                                }
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
                    catch (Exception)
                    {
                        // The EntityInstance property throws for some reason if it is null - SMH
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