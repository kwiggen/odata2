using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData;
using System.Web.OData.Formatter.Deserialization;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using odata2.Controllers;
using odata2.Models;

namespace odata2.Lib
{
    public class CourseDeserializer : ODataEntityDeserializer
    {
        public CourseDeserializer(ODataDeserializerProvider deserializerProvider) : base(deserializerProvider)
        {
        }

        public override object ReadEntry(ODataEntryWithNavigationLinks entryWrapper, 
                                         IEdmEntityTypeReference entityType, 
                                         ODataDeserializerContext readContext)
        {
            //we know that Teacher and Location can bothe be sent in as a @odata.id instead of an object
            //we need to look to see if the ID is sent in for these EntityType and return the object
            var objectType = entryWrapper.Entry.TypeName;
            if (objectType == "odata2.Models.Teacher")
            {
                var possibleTeacher = FindObject<Teacher>(entryWrapper.Entry.Id, TEACHER_URI_PREFIX,
                                                          TEACHER_URI_POSTFIX, TeachersController.getTeacher);
                if (possibleTeacher != null)
                    return possibleTeacher;
            }
            else if (objectType == "odata2.Models.Location")
            {
                var possibleFile = FindObject<Location>(entryWrapper.Entry.Id, FILE_URI_PREFIX,
                                                       FILE_URI_POSTFIX, Location.getFile);
                if (possibleFile != null)
                    return possibleFile;
            }
            else if (objectType == "odata2.Models.ExternalLocation")
            {
                var entryId = entryWrapper.Entry.Id;
                if (entryId != null)
                {
                    ExternalLocation externalLocation = new ExternalLocation();
                    externalLocation.Id = entryId.AbsoluteUri;
                    return externalLocation;
                }
            }

            //return the super
            return base.ReadEntry(entryWrapper, entityType, readContext);
        }


        ///////////////////////
        // Private
        ///////////////////////

        private static T FindObject<T>(Uri objectId, string prefix, string postfix, Func<int, T> lookupMethod)
        {
            if (objectId != null)
            {
                string absoluteUri = objectId.AbsoluteUri;
                //make sure that the prefix and postfix are correct, otherwise this is not our object
                if (absoluteUri.StartsWith(prefix) && absoluteUri.EndsWith(postfix))
                {
                    string idString = absoluteUri.Remove(0, prefix.Length);
                    idString = idString.Substring(0, idString.Length - postfix.Length);
                    try
                    {
                        int idInt = Convert.ToInt32(idString);
                        var found = lookupMethod(idInt);
                        if (found != null)
                            return found;
                    }
                    catch (FormatException)
                    {
                        //just fall through, let the real parser handle this
                    }
                }
            }
            //if we get here it wasn't our URI
            return default(T);
        }

        private static string TEACHER_URI_PREFIX = "http://localhost:22970/Teachers(";
        private static string TEACHER_URI_POSTFIX = ")";
        private static string FILE_URI_PREFIX = "http://localhost:22970/Locations(";
        private static string FILE_URI_POSTFIX = ")";
    }
}