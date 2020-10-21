using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Models.ImageUploadModels
{
    public class ImageUploadListItem
    {
        [Key]
        public int ImageId { get; set; }
        public string Title { get; set; }

        [DisplayName("Upload File")]
        public string ImageName { get; set; }
    }
}
