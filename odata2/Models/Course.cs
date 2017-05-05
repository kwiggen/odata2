using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web.OData.Builder;

namespace odata2.Models
{
    [DataContract(Name = "course")]
    public class Course
    {

        public Course()
        {
            Resources = new List<ResourceWrapper>();
        }

        public Course(int p_id, string p_name)
        {
            Id = p_id;
            DisplayName = p_name;
            Resources = new List<ResourceWrapper>();
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public Teacher Teacher { get; set; }

        [DataMember]
        [AutoExpand]
        [Contained]
        public ICollection<ResourceWrapper> Resources { get; set; }

    }
}