using BlazorPortfolio.Models;
using System;
using System.Net.Http.Json;

namespace BlazorPortfolio.Services;

public class GitHubService
{
    private readonly HttpClient _http;

    public GitHubService()
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri("https://api.github.com/")
        };
        // IMPORTANT : on n’essaie pas de définir User-Agent en WASM ;
        // le navigateur en envoie déjà un. Ajouter seulement Accept / Version :
        _http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.github+json");
        _http.DefaultRequestHeaders.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
    }

    public async Task<List<dynamic>> GetAllRepos(string owner)
    {
        var all = new List<dynamic>();
        var page = 1;
        while (true)
        {
            var url = $"users/{owner}/repos?per_page=100&page={page}&type=all&sort=updated";
            var list = await _http.GetFromJsonAsync<List<dynamic>>(url);
            if (list is null || list.Count == 0) break;
            all.AddRange(list);
            page++;
            if (list.Count < 100) break;
        }
        return all;
    }

    public async Task<Dictionary<string, long>> GetLanguagesAgg(string owner)
    {
        var agg = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase);
        var repos = await GetAllRepos(owner);

        foreach (var r in repos)
        {
            string name = r.name;
            var langs = await _http.GetFromJsonAsync<Dictionary<string, long>>($"repos/{owner}/{name}/languages");
            if (langs is null) continue;
            foreach (var kv in langs)
                agg[kv.Key] = agg.TryGetValue(kv.Key, out var v) ? v + kv.Value : kv.Value;
        }
        return agg;
    }

    public async Task<(int workflows, int runs, int success, int failure, double passRate)> GetActionsSummary(string owner)
    {
        int workflows = 0, runs = 0, success = 0, failure = 0;
        var repos = await GetAllRepos(owner);
        var since = DateTime.UtcNow.AddDays(-30);

        foreach (var r in repos)
        {
            string name = r.name;

            // workflows
            var wf = await _http.GetFromJsonAsync<dynamic>($"repos/{owner}/{name}/actions/workflows");
            workflows += (int?)wf?.total_count ?? 0;

            // runs (paginer un peu mais rester raisonnable pour la limite 60/h)
            var page = 1;
            for (int i = 0; i < 3; i++) // max 3 pages/rép pour limiter le volume
            {
                var runsRes = await _http.GetFromJsonAsync<dynamic>(
                    $"repos/{owner}/{name}/actions/runs?per_page=100&page={page}");
                var total = (int?)runsRes?.total_count ?? 0;
                if (total == 0) break;

                foreach (var run in runsRes.workflow_runs)
                {
                    runs++;
                    DateTime created = run.created_at;
                    if (created < since) continue;
                    string? conclusion = run.conclusion;
                    if (string.Equals(conclusion, "success", StringComparison.OrdinalIgnoreCase)) success++;
                    else if (string.Equals(conclusion, "failure", StringComparison.OrdinalIgnoreCase)) failure++;
                }

                if (((IEnumerable<object>)runsRes.workflow_runs).Count() < 100) break;
                page++;
            }
        }

        var passRate = runs == 0 ? 0 : Math.Round(success * 100.0 / runs, 1);
        return (workflows, runs, success, failure, passRate);
    }

    // Services/GitHubService.cs
    public async Task<List<Repo>> GetAllReposBis(string owner)
    {
        var all = new List<Repo>();
        var page = 1;

        while (true)
        {
            var url = $"users/{owner}/repos?per_page=100&page={page}&type=all&sort=updated";
            var list = await _http.GetFromJsonAsync<List<Repo>>(url);

            if (list is null || list.Count == 0) break;

            all.AddRange(list);
            page++;

            // La pagination de GitHub s'arrête si la page a moins de 100 éléments
            if (list.Count < 100) break;
        }

        return all;
    }

    // Services/GitHubService.cs
    public async Task<List<KeyValuePair<string, long>>> GetLanguagesAggBis(string owner)
    {
        var allLanguages = new Dictionary<string, long>();

        // 1. D'abord, on récupère la liste de tous les dépôts
        var repos = await GetAllReposBis(owner);

        // 2. Pour chaque dépôt, on fait une requête pour obtenir ses langages
        foreach (var repo in repos)
        {
            var url = $"repos/{owner}/{repo.Name}/languages";
            try
            {
                // La réponse de cette API est un dictionnaire (clé: langage, valeur: octets)
                var languagesForRepo = await _http.GetFromJsonAsync<Dictionary<string, long>>(url);
                if (languagesForRepo != null)
                {
                    // 3. On agrège les octets pour chaque langage
                    foreach (var kvp in languagesForRepo)
                    {
                        if (allLanguages.ContainsKey(kvp.Key))
                        {
                            allLanguages[kvp.Key] += kvp.Value;
                        }
                        else
                        {
                            allLanguages[kvp.Key] = kvp.Value;
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Gérer l'erreur si un dépôt est vide ou ne peut pas être accédé
                Console.WriteLine($"Could not get languages for repo {repo.Name}: {ex.Message}");
            }
        }

        // On retourne la liste agrégée
        return allLanguages.ToList();
    }

    // Services/GitHubService.cs
    public async Task<(int workflows, int runs, int success, int failure, double passRate)> GetActionsSummaryBis(string owner)
    {
        int workflows = 0, runs = 0, success = 0, failure = 0;
        var since = DateTime.UtcNow.AddDays(-30);

        // MODIFICATION : Appel à la méthode fortement typée
        var repos = await GetAllReposBis(owner);

        foreach (var repo in repos)
        {
            // workflows
            // MODIFICATION : Création d'un modèle pour les workflows
            var wf = await _http.GetFromJsonAsync<WorkflowResponse>($"repos/{owner}/{repo.Name}/actions/workflows");
            workflows += wf?.TotalCount ?? 0;

            // runs
            var page = 1;
            for (int i = 0; i < 3; i++)
            {
                // MODIFICATION : Création d'un modèle pour les runs
                var runsRes = await _http.GetFromJsonAsync<WorkflowRunsResponse>(
                    $"repos/{owner}/{repo.Name}/actions/runs?per_page=100&page={page}");

                if (runsRes?.WorkflowRuns is null || runsRes.WorkflowRuns.Count == 0) break;

                foreach (var run in runsRes.WorkflowRuns)
                {
                    if (run.CreatedAt < since) continue;
                    runs++;
                    if (string.Equals(run.Conclusion, "success", StringComparison.OrdinalIgnoreCase)) success++;
                    else if (string.Equals(run.Conclusion, "failure", StringComparison.OrdinalIgnoreCase)) failure++;
                }

                if (runsRes.WorkflowRuns.Count < 100) break;
                page++;
            }
        }
        var passRate = runs == 0 ? 0 : Math.Round(success * 100.0 / runs, 1);
        return (workflows, runs, success, failure, passRate);
    }
}
