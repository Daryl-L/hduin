using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class AccessTokenData
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string accessToken { get; set; }

        [DataMember]
        public string error { get; set; }
    }
}
