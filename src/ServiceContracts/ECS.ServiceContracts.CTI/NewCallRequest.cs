using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ECS.ServiceContracts.CTI
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [DataContract]
    public class NewCallRequest
    {
        [DataMember(Name = "ucid", IsRequired = true)]
        public string UCID { get; set; }

        [DataMember(Name = "dnis", IsRequired = true)]
        public string DNIS { get; set; }

        [DataMember(Name = "ani", IsRequired = true)]
        public string ANI { get; set; }
    }
}
