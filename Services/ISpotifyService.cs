using HUB.Models;

namespace HUB.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken);
        Task GetNewReleases();
    }
}
