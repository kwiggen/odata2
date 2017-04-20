using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract]
    public class Teacher
    {

        public Teacher() { }

        public Teacher(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}