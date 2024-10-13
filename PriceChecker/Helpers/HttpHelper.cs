using System.Collections.Concurrent;
using System.Net.Http;


namespace PriceChecker.Helpers
{
    public class HttpHelper
    {
        HttpClient _httpClient;

        public HttpHelper(HttpClient httpclient)
        {
            _httpClient= httpclient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
        }

        public async Task<ConcurrentDictionary<string, string>> GetMultipleRequests(IEnumerable<string> inputUrls)
        {
            List<Task> tasks = new();
            ConcurrentDictionary<string,string> responses = new();

            foreach (var url in inputUrls)
            {
                tasks.Add(Task.Run(async () =>
                {
                    responses[url] = await GetHttpGet(url);
                }));
            }

            await Task.WhenAll(tasks);
            return responses;
        }


        private async Task<string> GetHttpGet(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync() ?? string.Empty;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return string.Empty;
            }
        }
    }
}
