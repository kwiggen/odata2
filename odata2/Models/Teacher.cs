using System.Runtime.Serialization;

namespace odata2.Models
{
    [DataContract(Name = "teacher")]
    public class Teacher
    {

        public Teacher() { }

        public Teacher(int id, string name)
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