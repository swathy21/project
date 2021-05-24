using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using CafeAPITests.Models;


namespace CafeAPITests
{
    public class CafeAPIHelper
    {
        HttpClient client = new HttpClient();
        private string url = "http://localhost:5000/cafes/";

        public void ShowCafe(Cafe cafe)
        {
            Console.WriteLine($"CustomerName: {cafe.CustomerName}\tCafeId: " +
                $"{cafe.CafeId}\t: {cafe.FoodName}");
        }

        public async Task<Cafe> CreateCafeAsync(Cafe newCafe)
        {
            var content = new StringContent(newCafe.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{url}",  content);
            var cafeData = await response.Content.ReadAsStringAsync();
            var cafe= JsonConvert.DeserializeObject<Cafe>(cafeData);
            return cafe;
        }

        public async Task<Cafe> GetCafeAsync(int id)
        {
            Cafe cafe= null;
            HttpResponseMessage response = await client.GetAsync($"{url}{id}");
            if (response.IsSuccessStatusCode)
            {
                //student = await response.Content.ReadAsAsync<Student>();
                var cafeStrData = await response.Content.ReadAsStringAsync();
                cafe = JsonConvert.DeserializeObject<Cafe>(cafeStrData);
            }
            return cafe;
        }

        public async Task<List<Cafe>> GetCafesAsync()
        {
            List<Cafe> cafes= null;
            HttpResponseMessage response = await client.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                //student = await response.Content.ReadAsAsync<Student>();
                var cafeStrData = await response.Content.ReadAsStringAsync();
                cafes= JsonConvert.DeserializeObject<List<Cafe>>(cafeStrData);
            }
            return cafes;
        }

        public async Task<Cafe> UpdateCafeAsync(Cafe updCafe)
        {
            var content = new StringContent(updCafe.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"{url}", content);
            var cafeStrData = await response.Content.ReadAsStringAsync();
            var cafeData = JsonConvert.DeserializeObject<Cafe>(cafeStrData);
            return cafeData;
        }

        public async Task<int> DeleteCafeAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{url}{id}");
            var delData = await response.Content.ReadAsStringAsync();
            var numOfRows = int.Parse(delData);
            return numOfRows;
        }
    }

}