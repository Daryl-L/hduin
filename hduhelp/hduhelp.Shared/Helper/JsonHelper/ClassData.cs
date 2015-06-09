using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class ClassData
    {
        [DataMember]
        public string xkkh { get; set; }
        [DataMember]
        public string kcdm { get; set; }
        [DataMember]
        public string kcmc { get; set; }
        [DataMember]
        public string jsxm { get; set; }
        [DataMember]
        public string skdd { get; set; }
        [DataMember]
        public string qsz { get; set; }
        [DataMember]
        public string jsz { get; set; }
        [DataMember]
        public string xqj { get; set; }
        [DataMember]
        public string dsz { get; set; }
        [DataMember]
        public string beginat { get; set; }
        [DataMember]
        public string endat { get; set; }
    }
}
