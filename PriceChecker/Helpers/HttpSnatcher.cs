using System.Collections.Concurrent;
using System.Net.Http;
using PriceChecker.Iterfaces;


namespace PriceChecker.Helpers
{
    public class HttpSnatcher : IResourceSnatcher
    {
        HttpClient _httpClient;
        IAppLogger _logger;

        public HttpSnatcher(HttpClient httpclient, IAppLogger messageLogger)
        {
            _logger= messageLogger;
            _httpClient= httpclient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36");
        }

        public async Task<Dictionary<string, string>> GetUrls(IEnumerable<string> urls)
        {
            List<Task> tasks = new();
            ConcurrentDictionary<string, string> responses = new();

            foreach (var url in urls)
            {
                tasks.Add(Task.Run(async () =>
                {
                    responses[url] = await GetHttpGet(url);
                }));
            }

            await Task.WhenAll(tasks);
            return responses.ToDictionary();
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
                _logger.AlertFailure($"Error getting a http request {e}");
                return string.Empty;
            }
        }
    }
}
