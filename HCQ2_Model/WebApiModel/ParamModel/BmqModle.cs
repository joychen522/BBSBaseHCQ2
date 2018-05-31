using System.ComponentModel.DataAnnotations;

namespace HCQ2_Model.WebApiModel.ParamModel
{
    public class BmqModle
    {
        [Required]
        public int page { get; set; }

        [Required]
        public int rows { get; set; }

        public string search { get; set; }
    }

    public class BmqDetail
    {

        [Required]
        public string document_id { get; set; }
    }
}
