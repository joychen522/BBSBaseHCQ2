//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCQ2_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_AskManager
    {
        public int ask_id { get; set; }
        public string ask_unit { get; set; }
        public string user_name { get; set; }
        public System.DateTime ask_startDate { get; set; }
        public Nullable<System.DateTime> ask_endDate { get; set; }
        public string ask_day { get; set; }
        public string ask_title { get; set; }
        public string ask_context { get; set; }
        public string ask_type { get; set; }
        public string ask_status { get; set; }
        public string ask_operUser { get; set; }
        public Nullable<System.DateTime> ask_operDate { get; set; }
    }
}
