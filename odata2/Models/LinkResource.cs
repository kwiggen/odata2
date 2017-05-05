using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name = "linkResource")]
    public class LinkResource : Resource
    {

        public LinkResource()
        {
        }

        public LinkResource(string name, string link) : base(name)
        {
            Link = link;
        }

        [DataMember]
        public string Link { get; set; }

    }
}