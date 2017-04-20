using System.Runtime.Serialization;


namespace odata2.Models
{
    [DataContract]
    public class ExternalLocation
    {
        public ExternalLocation()
        {
        }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}