using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class HduActivityData
    {
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string root { get; set; }
        [DataMember]
        public Data[] list { get; set; }
        [DataContract]
        public class Data
        {
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string link { get; set; }
            [DataMember]
            public string time { get; set; }
        }
    }
}
