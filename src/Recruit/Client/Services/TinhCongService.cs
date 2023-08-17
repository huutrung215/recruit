using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Recruit.Client.Services;
using Recruit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Recruit.Server.Models;
using Recruit.Server.Models.ModelView;

namespace Recruit.Client.Services
{
    public class TinhCongService : ITinhCongService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public TinhCongService(HttpClient httpClient,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<int> updateTinhNgayCong(NgayCong ngayCong)
        {
            //string jsonObject = JsonSerializer.Serialize(ngayCong);
            //var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsJsonAsync($"/NgayCong/Update", ngayCong);
            var authResult = await response.Content.ReadFromJsonAsync<int>();
            return authResult;
        }

        public async Task<List<NgayCongView>> layNgayCongToanBoNV(DateTime fromDay, DateTime toDay, string MaNhanVien)
        {
            var response = await _httpClient.PostAsJsonAsync($"/Search", new DayToSearch() { FromDay = fromDay, ToDay = toDay, MaNhanVien = MaNhanVien });
            var authResult = await response.Content.ReadFromJsonAsync< List<NgayCongView>>();
            return authResult ?? new List<NgayCongView>();
        }

        public async Task<List<NhanVien>> getsNhanVien()
        {
            var response = await _httpClient.GetFromJsonAsync<List<NhanVien>>($"/getsNhanVien");
            return response ?? new List<NhanVien>();
        }

        public async Task<NhanVien> layNhanvienbyEmail(string email)
        {
            var response = await _httpClient.GetFromJsonAsync<NhanVien>($"/layNhanvienbyEmail/{email}");
            return response ?? new NhanVien();
        }

        public async Task<NgayCong?> getNgayCong(string maNV, string ngay, string thang, string nam)
        {
            var response = await _httpClient.GetFromJsonAsync<NgayCong>($"/getNgayCong/{maNV}/{ngay}/{thang}/{nam}");
            return response;
        }

        //public NgayCong GetNgayCong(int STT, string thang, string nam)
        //{
        //    return tinhCongDbContext.NgayCongs.FirstOrDefault(e => e.STT == STT && e.Nam == nam && e.Thang == thang);
        //}

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

        //public int UpdateNgayChamCong(NgayCong model)
        //{
        //    var ngayCong = GetNgayCong(model.STT, model.Thang, model.Nam);
        //    if (ngayCong == null)
        //    {
        //        return -1;
        //    }
        //    if(!string.IsNullOrEmpty(model.D1))
        //        ngayCong.D1 = model.D1;
        //    if(!string.IsNullOrEmpty(model.D2))
        //        ngayCong.D2 = model.D2;
        //    if(!string.IsNullOrEmpty(model.D3))
        //        ngayCong.D3 = model.D3;
        //    if(!string.IsNullOrEmpty(model.D4))
        //        ngayCong.D4 = model.D4;
        //    if(!string.IsNullOrEmpty(model.D5))
        //        ngayCong.D5 = model.D5;
        //    if(!string.IsNullOrEmpty(model.D6))
        //        ngayCong.D6 = model.D6;
        //    //if(string.IsNullOrEmpty(model.D1))
        //    //if(string.IsNullOrEmpty(model.D1))
        //    if(!string.IsNullOrEmpty(model.D7))
        //        ngayCong.D7 = model.D7;
        //    if(!string.IsNullOrEmpty(model.D8))
        //        ngayCong.D8 = model.D8;
        //    if(!string.IsNullOrEmpty(model.D9))
        //        ngayCong.D9 = model.D9;
        //    if(!string.IsNullOrEmpty(model.D10))
        //        ngayCong.D10 = model.D10;
        //    if(!string.IsNullOrEmpty(model.D11))
        //        ngayCong.D11 = model.D11;
        //    if(!string.IsNullOrEmpty(model.D12))
        //        ngayCong.D12 = model.D12;
        //    //if (!string.IsNullOrEmpty(model.GetType().GetProperty("D13").GetValue())){

        //    //}

        //        tinhCongDbContext.Update(ngayCong);
        //    return tinhCongDbContext.SaveChanges();
        //}
    }
}
