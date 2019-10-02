using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStore.Entities.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BookStore.WebClient
{
    public class ApiClient
    {
        public string BooksApiMethod = "/api/Books";
        public string BooksTopApiMethod = "/api/Books?$orderby=InsertDate%20desc&$top=7";
        public string AuthorsApiMethod = "/api/Authors";
        public string CategoriesApiMethod = "/api/Categories";
        public string CategoriesTopApiMethod = "/api/Categories?$orderby=InsertDate%20desc&$top=7";
        public string ReviewsApiMethod = "/api/Reviews";
        public string ReviewsTopApiMethod = "/api/Reviews?$orderby=InsertDate%20desc&$top=7";
        private string TokenApiMethod = "/api/Auth/Token";
        public string UsersApiMethod = "/api/Users";

        private readonly string _hostUrl;
        private readonly string _userName;
        private readonly string _password;

        private static string _theApiToken;
        private string _apiToken => _theApiToken ?? (_theApiToken = GetToken().Result);

        private readonly IHttpClientFactory _httpClientFactory;

        public ApiClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            _hostUrl = configuration["BookStore.Api.Host"];
            _userName = configuration["BookStore.Api.User"];
            _password = configuration["BookStore.Api.Password"];
        }

        public async Task<IList<T>> GetAllAsync<T>(string apiMethod)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var response = await httpClient.GetAsync($"{apiMethod}");

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<IList<T>>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return default;
        }

        public async Task<T> GetByIdAsync<T>(string apiMethod, int? id)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var response = await httpClient.GetAsync($"{apiMethod}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return default;
        }

        public async Task<T> EditAsync<T>(string apiMethod, int? id, T model)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var response = await httpClient.PutAsync($"{apiMethod}/{id}", model, new JsonMediaTypeFormatter());

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return default;
        }

        public async Task<T> CreateAsync<T>(string apiMethod, T model)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    var response = await httpClient.PostAsync($"{apiMethod}", model, new JsonMediaTypeFormatter());

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return default;
        }

        public async Task DeleteAsync(string apiMethod, int? id)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    await httpClient.DeleteAsync($"{apiMethod}/{id}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

            httpClient.Timeout = new TimeSpan(0, 0, 2500);

            httpClient.BaseAddress = new Uri(_hostUrl.EndsWith("/") ? _hostUrl : $"{_hostUrl}/");
            
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
           //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiToken);
           httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {_apiToken}");

            return httpClient;
        }

        private async Task<string> GetToken()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");

                    httpClient.Timeout = new TimeSpan(0, 0, 2500);

                    httpClient.BaseAddress = new Uri(_hostUrl.EndsWith("/") ? _hostUrl : $"{_hostUrl}/");

                    var response = await httpClient.PostAsync($"{TokenApiMethod}", new Auth
                    {
                        UserName = _userName,
                        Password = _password
                    }, new JsonMediaTypeFormatter());

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return string.Empty;
        }
    }
}