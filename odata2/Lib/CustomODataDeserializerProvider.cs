using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.OData.Formatter.Deserialization;
using Microsoft.OData.Edm;

namespace odata2.Lib
{
    public class CustomODataDeserializerProvider : DefaultODataDeserializerProvider
    {

        public CustomODataDeserializerProvider()
        {
            courseDeserializer = new CourseDeserializer(this);
        }

        public override ODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType)
        {
            return courseDeserializer;
        }

        private CourseDeserializer courseDeserializer;
    }
}