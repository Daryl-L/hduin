using System;
using System.Collections.Generic;
using System.Text;

namespace hduhelp.Model.DataModel
{
    class QueryTitleDataModel
    {
        private string _title;

        public QueryTitleDataModel(string title)
        {
            _title = title;
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }
    }
}
