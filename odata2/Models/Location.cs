
using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name = "location")]
    public class Location
    {
        public static Location getFile(int key)
        {
            //always return a Location
            return new Location(key, "Patrick Hall");
        }

        public Location()
        {
        }

        public Location(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

    }
}