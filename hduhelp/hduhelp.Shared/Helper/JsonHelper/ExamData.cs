using System.Runtime.Serialization;

namespace hduhelp.Helper.JsonHelper
{
    [DataContract]
    class ExamData
    {
        [DataMember]
        public string xkkh { get; set; }
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
