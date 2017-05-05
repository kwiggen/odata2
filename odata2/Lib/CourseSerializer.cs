
using System;
using System.Web.OData;
using System.Web.OData.Formatter.Serialization;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using odata2.Models;
using System.Reflection;
using System.Web.OData.Formatter;

namespace odata2.Lib
{
    public class CourseSerializer : ODataEntityTypeSerializer
    {
        public CourseSerializer(ODataSerializerProvider serializerProvider) : base(serializerProvider)
        {
        }

        public override ODataProperty CreateStructuralProperty(IEdmStructuralProperty structuralProperty, EntityInstanceContext entityInstanceContext)
        {
            // Skip the 'id' property on ExternalLocations and Resource
            if (SkipStructualProperty(structuralProperty))
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
            // Force the '@odata.id' property on ExternalLocation, Files
            ODataEntry entry = base.CreateEntry(selectExpandNode, entityInstanceContext);
            if (entry != null)
            {
                string id = GetIdForOdataIds(entry, entityInstanceContext);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    entry.Id = new Uri(id);
                    if (entityInstanceContext.SerializerContext.MetadataLevel == ODataMetadataLevel.FullMetadata)
                    {
                        entry.EditLink = null;
                    }
                }
            }
            return entry;
        }

        //List of Entities we do NOT want the id serialized out to the Client
        //This is either due to the fact that we are replacing the id with @odata.id OR
        //We have a complex type being represeted as an Entity.  In the second case we need to not print
        //the id for the Complex type.  This Complex type issue is resolved in a future version of the odata lib
        private static Boolean SkipStructualProperty(IEdmStructuralProperty structuralProperty)
        {
            return ((StringUtilities.InvariantInsensitive(structuralProperty.Name, "id") == 0 &&
                     StringUtilities.InvariantInsensitive(structuralProperty.DeclaringType.FullTypeName(),
                                                          typeof(ExternalLocation).FullName) == 0) ||
                    (StringUtilities.InvariantInsensitive(structuralProperty.Name, "id") == 0 &&
                     StringUtilities.InvariantInsensitive(structuralProperty.DeclaringType.FullTypeName(),
                                                          typeof(Resource).FullName) == 0) ||
                    (StringUtilities.InvariantInsensitive(structuralProperty.Name, "id") == 0 &&
                     StringUtilities.InvariantInsensitive(structuralProperty.DeclaringType.FullTypeName(),
                                                          typeof(File).FullName) == 0));
        }      
        
        //These are the objects that need to display the @odata.id insteado of ID.  They need to be in the above
        //list as well as here.  One removes the id and this adds the @odata.id properties
        private static String GetIdForOdataIds(ODataEntry entry, EntityInstanceContext entityInstanceContext)
        {
            string id = string.Empty;
            if (StringUtilities.InvariantInsensitive(entry.TypeName, typeof(ExternalLocation).FullName) == 0)
            {
                ExternalLocation extLocation = null;
                try
                {
                    extLocation = entityInstanceContext.EntityInstance as ExternalLocation;
                    if (extLocation != null)
                    {
                        id = extLocation.Id;
                    }
                }
                catch (Exception)
                {
                    // The EntityInstance property throws in some circumstances
                    // For example if the FisplayMember attribute has changed the property's name.
                    // Fall back to the EdmObject property for resilience.
                    // All patterns I could test are an internal object with an 'Instance' property
                    object locationBinder = entityInstanceContext.EdmObject;
                    PropertyInfo instanceProperty = locationBinder.GetType().GetProperty("Instance", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
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
                }
            } else if (StringUtilities.InvariantInsensitive(entry.TypeName, typeof(File).FullName) == 0)
            {
                File file = null;
                try
                {
                    file = entityInstanceContext.EntityInstance as File;
                    if (file != null)
                    {
                        id = file.Id;
                    }
                }
                catch (Exception)
                {
                    // The EntityInstance property throws in some circumstances
                    // For example if the FisplayMember attribute has changed the property's name.
                    // Fall back to the EdmObject property for resilience.
                    // All patterns I could test are an internal object with an 'Instance' property
                    object locationBinder = entityInstanceContext.EdmObject;
                    PropertyInfo instanceProperty = locationBinder.GetType().GetProperty("Instance", BindingFlags.FlattenHierarchy | 
                                                                                         BindingFlags.Public | BindingFlags.Instance);
                    if (instanceProperty != null)
                    {
                        object rawLocation = instanceProperty.GetValue(locationBinder);
                        if (rawLocation != null)
                        {
                            file = rawLocation as File;
                            if (file != null)
                            {
                                id = file.Id;
                            }
                        }
                    }
                }
            }
            return id;
        }
    }
}