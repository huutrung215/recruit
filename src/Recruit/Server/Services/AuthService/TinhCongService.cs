using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Recruit.Server.Data;
using Recruit.Server.Models;
using Recruit.Server.Models.ModelView;
using Recruit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruit.Server.Services.AuthService
{
    public class TinhCongService : ITinhCongService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext tinhCongDbContext;

        public TinhCongService(UserManager<ApplicationUser> userManager, ApplicationDbContext tinhCongDbContext)
        {
            this.userManager = userManager;
            this.tinhCongDbContext = tinhCongDbContext;
        }


        public int tinhNgayCong(NgayCong ngayCong)
        {
            NgayCongDb ngayCongNew = new NgayCongDb()
            {
                STT = ngayCong.STT,
                MaNV = ngayCong.MaNV,
                Thang = ngayCong.Thang,
                Nam = ngayCong.Nam,
                Ngay = ngayCong.Ngay,
                TrangThai = ngayCong.TrangThai
            };
            tinhCongDbContext.NgayCongs.Add(ngayCongNew);
            return tinhCongDbContext.SaveChanges();
        }

        public IEnumerable<NgayCongView?> layNgayCongToanBoNV(DayToSearch dayToSearch)
        {
            var toDay = dayToSearch.ToDay;
            var fromDay = dayToSearch.FromDay;
            List<NgayCongView?> results = new List<NgayCongView?>();
            if (string.IsNullOrEmpty(dayToSearch.MaNhanVien))
            {
                var nhanviens = getsNhanVien();
                foreach(var nhanvien in nhanviens)
                {
                    results.Add(getNgayCongsCommon(new DayToSearch()
                    {
                        MaNhanVien = nhanvien.MaNV,
                        FromDay = fromDay,
                        ToDay = toDay
                    }));
                }
                return results;
            }
            results.Add(getNgayCongsCommon(dayToSearch));
            return results;

            //List<NgayCongView> ngayCong = new List<NgayCongView>();
            //if (dayToSearch.ToDay.Month == dayToSearch.FromDay.Month)
            //{
            //    for(int i = fromDay.Day; i <= toDay.Day; i++)
            //    {
            //        ngayCong.Add(getNgayCongCommon(new DateTime(fromDay.Year, fromDay.Month, i)));
            //    }
            //}
            //else
            //{
            //    int days = DateTime.DaysInMonth(fromDay.Year, fromDay.Month);
            //    for (int i = fromDay.Day; i <= days; i++)
            //    {
            //        ngayCong.Add(getNgayCongCommon(new DateTime(fromDay.Year, fromDay.Month, i)));
            //    }
            //    for (int i = 1; i <= toDay.Day; i++)
            //    {
            //        ngayCong.Add(getNgayCongCommon(new DateTime(toDay.Year, toDay.Month, i)));
            //    }
            //}

            //    //if(dayToSearch.ToDay.Month == dayToSearch.FromDay.Month)
            //    //{
            //    //    if (!string.IsNullOrEmpty(dayToSearch.MaNhanVien))
            //    //    {
            //    //        var ngayChamCongFrom = ngayCongFrom.Where(e => e.MaNV == dayToSearch.MaNhanVien).ToList();
            //    //        return new List<List<NgayCongView>>() { ngayChamCongFrom };
            //    //    }
            //    //    return new List<List<NgayCongView>>() { ngayCongFrom };
            //    //}
            //    //List<NgayCongView> ngayCongTo = getNgayCongCommon(dayToSearch.ToDay).ToList();
            //    //if (!string.IsNullOrEmpty(dayToSearch.MaNhanVien))
            //    //{
            //    //    var ngayChamCongFrom = ngayCongFrom.Where(e => e.MaNV == dayToSearch.MaNhanVien).ToList();
            //    //    var ngayChamCongTo = ngayCongTo.Where(e => e.MaNV == dayToSearch.MaNhanVien).ToList();
            //    //    return new List<List<NgayCongView>>() { ngayChamCongFrom, ngayChamCongTo };
            //    //}
            //    return ngayCong;
        }
        public NgayCongView? getNgayCongsCommon(DayToSearch dayToSearch)
        {
            List<NgayCongView> ngayCongs = new List<NgayCongView>();

            var toDay = dayToSearch.ToDay;
            var fromDay = dayToSearch.FromDay;

            var nhanVienCoNgayCong = (from nv in tinhCongDbContext.NhanViens
                                     where nv.MaNV == dayToSearch.MaNhanVien
                                     select new NgayCongView()
                                     {
                                         HoTen = nv.HoTen,
                                         MaNV = nv.MaNV,
                                     }).FirstOrDefault();
            List<ChamCongView> chamCongViews = new List<ChamCongView>();
            if (dayToSearch.ToDay.Month == dayToSearch.FromDay.Month)
            {
                for (int i = fromDay.Day; i <= toDay.Day; i++)
                {
                    var ngayCong = (from nc in tinhCongDbContext.NgayCongs where nc.MaNV == dayToSearch.MaNhanVien
                                    && nc.Ngay == new DateTime(toDay.Year, toDay.Month, i)
                                    select nc).FirstOrDefault();

                    chamCongViews.Add(new ChamCongView()
                    {
                        Nam = toDay.Year.ToString(),
                        Thang = toDay.Month.ToString(),
                        Ngay = i.ToString(),
                        TrangThai = ngayCong != null ? ngayCong.TrangThai : "",
                        Latitude = ngayCong != null ? ngayCong.Latitude : "",
                        Longitude = ngayCong != null ? ngayCong.Longitude : "",
                    });
                }
            }
            else
            {
                int days = DateTime.DaysInMonth(fromDay.Year, fromDay.Month);
                for (int i = fromDay.Day; i <= days; i++)
                {
                    var ngayCong = (from nc in tinhCongDbContext.NgayCongs
                                    where nc.MaNV == dayToSearch.MaNhanVien
                                    && nc.Ngay == new DateTime(fromDay.Year, fromDay.Month, i)
                                    select nc).FirstOrDefault();
                    chamCongViews.Add(new ChamCongView()
                    {
                        Nam = fromDay.Year.ToString(),
                        Thang = fromDay.Month.ToString(),
                        Ngay = i.ToString(),
                        TrangThai = ngayCong != null ? ngayCong.TrangThai : "",
                        Latitude = ngayCong != null ? ngayCong.Latitude : "",
                        Longitude = ngayCong != null ? ngayCong.Longitude : "",
                    });
                }
                for (int i = 1; i <= toDay.Day; i++)
                {
                    var ngayCong = (from nc in tinhCongDbContext.NgayCongs
                                    where nc.MaNV == dayToSearch.MaNhanVien
                                    && nc.Ngay == new DateTime(toDay.Year, toDay.Month, i)
                                    select nc).FirstOrDefault();
                    chamCongViews.Add(new ChamCongView()
                    {
                        Nam = toDay.Year.ToString(),
                        Thang = toDay.Month.ToString(),
                        Ngay = i.ToString(),
                        TrangThai = ngayCong != null ? ngayCong.TrangThai : "",
                        Latitude = ngayCong != null ? ngayCong.Latitude : "",
                        Longitude = ngayCong != null ? ngayCong.Longitude : "",
                    });
                }
            }
            nhanVienCoNgayCong.chamCongs = chamCongViews;
            return nhanVienCoNgayCong;
        }

        public NgayCongDb? GetNgayCong(NgayCong ngayCong)
        {
            return tinhCongDbContext.NgayCongs.FirstOrDefault(e => e.MaNV == ngayCong.MaNV && e.Ngay == ngayCong.Ngay);
        }

        //public int DeleteCustomer(int id)
        //{
        //    var delCus = GetCustomer(id);
        //    if (delCus != null)
        //    {
        //        context.Customers.Remove(delCus);
        //        return context.SaveChanges();
        //    }
        //    return -1;
        //}

        public int addUpdateNgayChamCong(NgayCong model)
        {
            var ngayCong = GetNgayCong(model);
            if (ngayCong == null)
            {
                ngayCong = new NgayCongDb()
                {
                    MaNV = model.MaNV,
                    Thang = model.Thang,
                    Nam = model.Nam,
                    Ngay = model.Ngay,
                    TrangThai = model.TrangThai,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };

                tinhCongDbContext.Add(ngayCong);
                return tinhCongDbContext.SaveChanges();
            }
            ngayCong.TrangThai = model.TrangThai;
            ngayCong.Latitude = model.Latitude;
            ngayCong.Longitude = model.Longitude;
            tinhCongDbContext.Update(ngayCong);
            return tinhCongDbContext.SaveChanges();
        }

        public IEnumerable<NhanVienDb> getsNhanVien()
        {
            return tinhCongDbContext.NhanViens;
        }

        public NhanVienDb? layNhanvienbyEmail(string email)
        {
            var acc = tinhCongDbContext.Users.Where(e => e.Email == email);
            var nv1 = tinhCongDbContext.NhanViens;

            var nhanvien = from user in userManager.Users
                           join nv in tinhCongDbContext.NhanViens on user.STT equals nv.STT
                           where user.Email == email
                           select nv;
            return nhanvien.FirstOrDefault();
        }

        public NgayCong? getNgayCong(string MaNV, string ngay, string thang, string nam)
        {
            var ngayCong = (from nc in tinhCongDbContext.NgayCongs
                            where nc.MaNV == MaNV
                            && nc.Ngay == new DateTime(Int32.Parse(nam), Int32.Parse(thang), Int32.Parse(ngay))
                            select nc).FirstOrDefault();
            if(ngayCong == null)
            {
                return null;
            }
            return new NgayCong()
            {
                MaNV = ngayCong.MaNV,
                Thang = ngayCong.Thang,
                Nam = ngayCong.Nam,
                Ngay = ngayCong.Ngay,
                TrangThai = ngayCong.TrangThai,
                Latitude = ngayCong.Latitude,
                Longitude = ngayCong.Longitude,
                STT = ngayCong.STT
            };
        }
    }
}
