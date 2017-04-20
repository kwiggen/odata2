using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name ="externalCourse")]
    public class ExternalCourse : Course
    {
        public ExternalCourse()
        {

        }
        public ExternalCourse(int p_id, string p_name) : base(p_id, p_name)
        {
        }

        [DataMember]
        public ExternalLocation Location { get; set; }
    }
}