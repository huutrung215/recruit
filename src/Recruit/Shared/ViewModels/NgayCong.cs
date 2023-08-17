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
        public int STT { get; set; }
        public string MaNV { get; set; } = "";
        public string Thang { get; set; } = "";
        public string Nam { get; set; } = "";
        public DateTime Ngay { get; set; }
        public string TrangThai { get; set; } = "";
        public string Latitude { get; set; } = "";
        public string Longitude { get; set; } = "";
    }
}
