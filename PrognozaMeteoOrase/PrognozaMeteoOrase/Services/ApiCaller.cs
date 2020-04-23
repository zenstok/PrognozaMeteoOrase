using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PrognozaMeteoOrase.Services
{
    public class ApiCaller
    {
        public static string API_KEY = "8a888fbbca041a651f3be4e2c123e936";
        public static async Task<ApiResponse> Get(string url, string authId = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(authId))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authId);

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                    return new ApiResponse { ErrorMessage = request.ReasonPhrase };
            }
        }
    }

    public class ApiResponse
    {
        public bool Successful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
