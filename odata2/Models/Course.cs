using System.Runtime.Serialization;
using System.Web.OData.Builder;

namespace odata2.Models
{
    [DataContract]
    public class Course
    {

        public Course()
        {
        }

        public Course(int p_id, string p_name)
        {
            Id = p_id;
            DisplayName = p_name;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name = "teacher")]
        //[Contained]
        public Teacher Teacher { get; set; }

    }
}