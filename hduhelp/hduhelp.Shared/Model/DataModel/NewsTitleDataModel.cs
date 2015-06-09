using System;
using System.Collections.Generic;
using System.Text;

namespace hduhelp.Model.DataModel
{
    class NewsTitleDataModel
    {
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public NewsTitleDataModel(string title)
        {
            _title = title;
        }
        
    }
}
