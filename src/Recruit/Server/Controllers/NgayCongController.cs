using Microsoft.AspNetCore.Mvc;
using Recruit.Server.Models;
using Recruit.Server.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgayCongController : Controller
    {
        private readonly ITinhCongService tinhCongService;

        public NgayCongController(ITinhCongService tinhCongService)
        {
            this.tinhCongService = tinhCongService;
        }

        [HttpPost("/Search")]
        public JsonResult Search(DayToSearch dayToSearch)
        {
            var result = tinhCongService.layNgayCongToanBoNV(dayToSearch);
            return Json(result);
        }

        [HttpGet("/getsNhanVien")]
        public JsonResult getsNhanVien()
        {
            var result = tinhCongService.getsNhanVien();
            return Json(result);
        }

        [HttpGet("/layNhanvienbyEmail/{email}")]
        public JsonResult layNhanvienbyEmail(string email)
        {
            var result = tinhCongService.layNhanvienbyEmail(email);
            return Json(result);
        }

        [HttpGet("/getNgayCong/{maNV}/{ngay}/{thang}/{nam}")]
        public JsonResult getNgayCong(string maNV, string ngay, string thang, string nam)
        {
            var result = tinhCongService.getNgayCong(maNV, ngay, thang, nam);
            return Json(result);
        }

        [HttpPost("/NgayCong/Update")]
        public JsonResult Update(NgayCong ngayCong)
        {
            var kqua = tinhCongService.addUpdateNgayChamCong(ngayCong);
            return Json(kqua);
        }
    }
}
