using HUB.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HUB.Services
{
    public class SpotifyService : ISpotifyService
    {
        public readonly HttpClient _httpClient;
        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}");
            Debug.WriteLine($"Response Status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
                return Enumerable.Empty<Release>();
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream);

            return responseObject?.albums?.items.Select(i => new Release
            {
                Name = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault()?.url,
                Link = i.external_urls.spotify,
                Artist = string.Join(",", i.artists.Select(a => a.name))
            }) ?? Enumerable.Empty<Release>();
        }
        public Task GetNewReleases()
        {
            throw new NotImplementedException();
        }
    }
}
