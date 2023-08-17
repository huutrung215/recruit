using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Server.Models
{
    public class NgayCong
    {
        [Key]
        public int STT { get; set; }
        [Column("MA_NV")]
        [Required]
        public string MaNV { get; set; }
        [Required]
        public string Thang { get; set; }
        [Required]
        public string Nam { get; set; }
        [Required]
        public DateTime Ngay { get; set; }
        public string TrangThai { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        //public virtual NhanVien? NhanVien { get; set; }
    }
}
