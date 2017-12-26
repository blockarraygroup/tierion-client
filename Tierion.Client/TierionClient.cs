using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tierion.Client
{
    //If you exceed the API rate limits, API requests will fail with an HTTP 429: Too Many Requests response.
    public class HashClient
    {
        private readonly string _username;
        private readonly string _password;

        public HashClient(string username, string password, string endPoint = "https://hashapi.tierion.com/v1/")
        {
            _username = username;
            _password = password;
            this.EndPoint = endPoint;
            if (!this.EndPoint.EndsWith("/"))
            {
                this.EndPoint += "/";
            }
        }

        public string EndPoint { get; set; }
        public string AccessToken { get; set; }
        protected virtual string GetUrl(string path)
        {
            return $"{this.EndPoint}{path}";
        }
        protected virtual async Task<T> Get<T>(string path, object request)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            var response = await httpClient.GetAsync((string) GetUrl(EndPoint));
            var obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }



        protected virtual async Task<T> Post<T>(string path, object request, bool includeAuth = true)
        {

            HttpClient httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (includeAuth)
            {
                
                if (string.IsNullOrEmpty(AccessToken) || IsTokenExpired)
                {
                    var result = await Auth();
                    
                }
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            }
                
            var body = JsonConvert.SerializeObject(request);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync((string) GetUrl(path),content);
            T obj = JsonConvert.DeserializeObject<T>(
                response.Content.ReadAsStringAsync().Result);
            return obj;
        }
        
        public async Task<AuthResponse> Auth()
        {
            var result =  await Post<AuthResponse>("auth/token", new AuthRequest() {username = _username, password = _password}, false);
            if (string.IsNullOrEmpty(result.access_token))
            {
                throw new Exception($"Login failed for user {_username}");
            }
            this.AccessToken = result.access_token;
            this.ExpiresAt = DateTime.Now.Add(TimeSpan.FromSeconds(result.expires_in - 20));
            this.RefreshToken = result.refresh_token;
            return result;
        }

        public bool IsTokenExpired => DateTime.Now >= ExpiresAt;

        public string RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}