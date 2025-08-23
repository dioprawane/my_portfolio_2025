using System.Net.Http.Json;
using BlazorPortfolio.Models;

namespace BlazorPortfolio.Services
{
    public class AnalyticsApiClient
    {
        /*private readonly HttpClient _http;
        public AnalyticsApiClient(HttpClient http) => _http = http;

        public Task<OverviewDto?> GetOverviewAsync(string owner, CancellationToken ct = default)
            => _http.GetFromJsonAsync<OverviewDto>($"/overview?owner={Uri.EscapeDataString(owner)}", ct);

        public async Task RefreshOverviewAsync(string owner, CancellationToken ct = default)
        {
            var resp = await _http.PostAsync($"/overview/refresh?owner={Uri.EscapeDataString(owner)}",
                                             content: null, ct);
            resp.EnsureSuccessStatusCode();
        }

        public async Task<string> GetStatusAsync(string owner, CancellationToken ct = default)
        {
            var dic = await _http.GetFromJsonAsync<Dictionary<string, string>>(
                $"/overview/status?owner={Uri.EscapeDataString(owner)}", ct);
            return dic?["status"] ?? "idle";
        }*/

        private readonly HttpClient _http;
        public AnalyticsApiClient(HttpClient http) => _http = http;

        // Rapide: lit le "latest" (fichier) du backend
        public Task<OverviewDto?> GetOverviewCachedAsync(string owner, CancellationToken ct = default) =>
            _http.GetFromJsonAsync<OverviewDto>($"/overview/cached?owner={Uri.EscapeDataString(owner)}", ct);

        // (si tu veux forcer un build synchrone + écrire un fichier à chaque fois)
        public Task<OverviewDto?> GetOverviewAsync(string owner, CancellationToken ct = default) =>
            _http.GetFromJsonAsync<OverviewDto>($"/overview?owner={Uri.EscapeDataString(owner)}", ct);

        // Lancer le rebuild (asynchrone)
        public async Task RefreshOverviewAsync(string owner, CancellationToken ct = default)
        {
            var resp = await _http.PostAsync($"/overview/rebuild?owner={Uri.EscapeDataString(owner)}", null, ct);
            resp.EnsureSuccessStatusCode();
        }

        // Polling statut
        public async Task<string> GetStatusAsync(string owner, CancellationToken ct = default)
        {
            var dic = await _http.GetFromJsonAsync<Dictionary<string, string>>(
                $"/overview/status?owner={Uri.EscapeDataString(owner)}", ct);
            return dic?["status"] ?? "idle";
        }

        // (optionnel) télécharger le nom de fichier latest si tu en as besoin
        public Task<HttpResponseMessage> DownloadLatestFileAsync(string owner, CancellationToken ct = default) =>
            _http.GetAsync($"/overview/file?owner={Uri.EscapeDataString(owner)}", ct);

        public Task<List<RepoDto>?> GetReposAsync(string owner, CancellationToken ct = default) =>
            _http.GetFromJsonAsync<List<RepoDto>>($"/repos?owner={Uri.EscapeDataString(owner)}", ct);
    }

}
