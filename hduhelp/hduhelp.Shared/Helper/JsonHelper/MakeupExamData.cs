using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class MakeupExamData
    {
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public Data[] list { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataContract]
        public class Data
        {
            [DataMember]
            public string kcmc { get; set; }
            [DataMember]
            public string kssj { get; set; }
            [DataMember]
            public string jsmc { get; set; }
            [DataMember]
            public string zwh { get; set; }
        }

    }
}
