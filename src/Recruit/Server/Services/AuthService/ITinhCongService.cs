using Recruit.Server.Models;
using Recruit.Server.Models.ModelView;
using Recruit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Server.Services.AuthService
{
    public interface ITinhCongService
    {
        int tinhNgayCong(NgayCong ngayCong);
        IEnumerable<NgayCongView?> layNgayCongToanBoNV(DayToSearch dayToSearch);
        NgayCongDb GetNgayCong(NgayCong ngayCong);
        int addUpdateNgayChamCong(NgayCong model);
        NgayCongView? getNgayCongsCommon(DayToSearch dayToSearch);
        IEnumerable<NhanVienDb> getsNhanVien();
        NhanVienDb layNhanvienbyEmail(string email);
        NgayCong getNgayCong(string MaNV, string ngay, string thang, string nam);
    }
}
