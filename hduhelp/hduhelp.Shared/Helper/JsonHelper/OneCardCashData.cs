using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class OneCardCashData
    {
        [DataMember]
        public string error { get; set; }

        [DataMember]
        public string balance { get; set; }

        //[DataMember]
        //public Data[] record { get; set; }

        //[DataContract]
        //public class Data
        //{
            
        //}
    }
}
