using System.Web.OData.Formatter.Serialization;
using Microsoft.OData.Edm;

namespace odata2.Lib
{
    public class CustomODataSerializerProvider : DefaultODataSerializerProvider
    {

        public CustomODataSerializerProvider()
        {
            courseSerializer = new CourseSerializer(this);
        }

        public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            if (edmType.IsEntity())
            {
                return courseSerializer;
            }
            return base.GetEdmTypeSerializer(edmType);

        }

        private CourseSerializer courseSerializer;
    }
}