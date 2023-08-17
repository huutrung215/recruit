using Recruit.Server.Models;
using Recruit.Server.Models.ModelView;
using Recruit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Client.Services
{
    public interface ITinhCongService
    {
        Task<int> updateTinhNgayCong(NgayCong ngayCong);
        Task<List<NgayCongView>> layNgayCongToanBoNV(DateTime fromDay, DateTime toDay, string MaNhanVien);
        Task<List<NhanVien>> getsNhanVien();
        Task<NgayCong?> getNgayCong(string MaNV, string ngay, string thang, string nam);
        Task<NhanVien> layNhanvienbyEmail(string email);
    }
}
