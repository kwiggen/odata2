using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name = "resource")]
    public abstract class Resource
    {

        public Resource()
        {
            Id = -1;
        }

        public Resource(string name)
        {
            Id = -1;
            DisplayName = name;
        }

        [DataMember]
        public int Id { get; set; }


        [DataMember]
        public string DisplayName { get; set; }
    }
}