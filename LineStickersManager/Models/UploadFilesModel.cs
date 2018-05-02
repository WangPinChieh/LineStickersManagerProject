using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LineStickersManager.Models
{
    public class UploadFilesModel
    {
        public string Product { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
    }
}