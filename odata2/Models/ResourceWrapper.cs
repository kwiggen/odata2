using System;
using System.Runtime.Serialization;
using System.Web.OData.Builder;

namespace odata2.Models
{
    [DataContract(Name = "resourceWrapper")]
    public class ResourceWrapper
    {

        public ResourceWrapper()
        {
        }

        public ResourceWrapper(int id, Boolean distribute, Resource wrapMe)
        {
            Id = id;
            DistributeResource = distribute;
            WrappedResource = wrapMe;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Boolean DistributeResource { get; set; }

        [DataMember]
        [AutoExpand]
        [Contained]
        public Resource WrappedResource { get; set; }
    }
}