using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class LibraryCenterPersonData
    {
        [DataMember]
        public LendMine lend_mine { get; set; }
        [DataMember]
        public ReservationMine reservation_mine { get; set; }

        [DataContract]
        public class LendMine
        {
            [DataMember]
            public Now[] now { get; set; }
            [DataMember]
            public History[] history { get; set; }

            [DataContract]
            public class Now
            {
                [DataMember]
                public string id_entity { get; set; }
                [DataMember]
                public string name { get; set; }
                [DataMember]
                public string author { get; set; }
                [DataMember]
                public string date_deadline { get; set; }
                [DataMember]
                public string times_renew { get; set; }
                [DataMember]
                public string date_surplus { get; set; }
            }

            [DataContract]
            public class History
            {
                [DataMember]
                public string id_book { get; set; }
                [DataMember]
                public string name { get; set; }
                [DataMember]
                public string author { get; set; }
                [DataMember]
                public string date_lend { get; set; }
                [DataMember]
                public string date_deadline { get; set; }
            }
        }

        [DataContract]
        public class ReservationMine
        {
            [DataMember]
            public Now[] now { get; set; }
            //[DataMember]
            //public History[] history { get; set; }

            [DataContract]
            public class Now
            {
                [DataMember]
                public string author { get; set; }
                [DataMember]
                public string name { get; set; }
                [DataMember]
                public string date_deadline { get; set; }
                [DataMember]
                public string date_surplus { get; set; }
            }
            //[DataContract]
            //public class History
            //{

            //}
        }
    }
}
