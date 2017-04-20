using System.Runtime.Serialization;
using System.Web.OData.Builder;

namespace odata2.Models
{
    [DataContract(Name = "course")]
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

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        //[Contained]
        public Teacher Teacher { get; set; }

    }
}