using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name = "file")]
    public class File
    {
        public File()
        {
        }

        [DataMember]
        public string Id { get; set; }
    }
}