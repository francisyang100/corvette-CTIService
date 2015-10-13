using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace ECS.ServiceContracts.CTI
{
    [DataContract]
    public class CallHistoryUpdateRequest
    {
        [DataMember(Name= "ucid" ,IsRequired = true)]
        public string UCID { get; set; }

        [DataMember(Name = "action", IsRequired = true)]
        public string Action { get; set; }

        [DataMember(Name = "action_data")]
        public string ActionData { get; set; }

        [DataMember(Name = "last_location")]
        public string LastVDNLocation { get; set; }
    }
}
