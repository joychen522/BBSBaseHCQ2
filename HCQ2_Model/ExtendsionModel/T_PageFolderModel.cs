using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQ2_Model.ExtendsionModel
{
    /// <summary>
    ///  菜单目录树模型
    /// </summary>
    public class T_PageFolderModel
    {
        public int folder_id { get; set; }
        public string text { get; set; }
        public string folder_url { get; set; }
        public string folder_image { get; set; }
        public int folder_order { get; set; }
        public bool have_child { get; set; }
        public string sm_code { get; set; }
        public List<T_PageFolderModel> nodes { get; set; }
    }
}
