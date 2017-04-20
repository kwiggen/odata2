using System.Runtime.Serialization;


namespace odata2.Models
{
    [DataContract(Name = "externalLocation")]
    public class ExternalLocation
    {
        public ExternalLocation()
        {
        }

        [DataMember]
        public string Id { get; set; }
    }
}