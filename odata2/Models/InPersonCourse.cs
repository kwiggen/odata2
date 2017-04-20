using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name ="course")]
    public class InPersonCourse : Course
    {
        public InPersonCourse()
        {

        }
        public InPersonCourse(int p_id, string p_name) : base(p_id, p_name)
        {
        }

        [DataMember]
        public Location Location { get; set; }
    }
}