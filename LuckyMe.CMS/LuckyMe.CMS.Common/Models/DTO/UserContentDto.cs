using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyMe.CMS.Common.Models.DTO
{
    public class UserContentDto
    {
        public virtual List<VideoDto> Videos { get; set; }
        public virtual List<AlbumDto> Albums { get; set; }
    }
}
