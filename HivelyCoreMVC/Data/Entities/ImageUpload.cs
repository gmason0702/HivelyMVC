using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Data.Entities
{
    public class ImageUpload
    {
        [Key]
        [DisplayName("Image ID")]
        public int ImageId{ get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
        
        [DisplayName("Upload File")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
