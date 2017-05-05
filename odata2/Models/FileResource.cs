using System.Runtime.Serialization;
using System.Web.OData.Builder;

namespace odata2.Models
{
    [DataContract(Name = "fileResource")]
    public class FileResource : Resource
    {
        public FileResource() 
        {
        }

        public FileResource(string name, string fileOdataId) : base(name)
        {
            GraphFile = new File();
            GraphFile.Id = fileOdataId;
        }

        [DataMember]
        [Contained]
        [AutoExpand]
        public File GraphFile { get; set; }
    }
}