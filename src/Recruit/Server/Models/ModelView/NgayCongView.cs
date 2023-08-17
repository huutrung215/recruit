using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Server.Models.ModelView
{
    public class NgayCongView
    {
        public int STT { get; set; }
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public List<ChamCongView>? chamCongs { get; set; }
    }
    public class ChamCongView
    {
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Ngay { get; set; }
        public string? TrangThai { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
