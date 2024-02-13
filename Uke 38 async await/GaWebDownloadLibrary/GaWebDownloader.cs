using System.Collections.Generic;
using System.Net;

namespace GaWebDownloadLibrary;

public class GaWebDownloader
{
    private readonly List<string> _urls = new();

    private void SetupWebList()
    {
        _urls.Clear();
        _urls.Add("https://www.vg.no");
        _urls.Add("https://www.google.no");
        _urls.Add("https://www.db.no");
        _urls.Add("https://github.com");
        _urls.Add("https://www.youtube.com");
        _urls.Add("https://www.facebook.com");
        _urls.Add("https://www.nettavisen.no");
    }

    #region Synkron
    public List<GaWebDownloadResult> StartDownloadSites()
    {
        return DownloadSites();
    }

    private List<GaWebDownloadResult> DownloadSites()
    {
        SetupWebList();
        List<GaWebDownloadResult> results = new();
        foreach (var url in _urls)
        {
            results.Add(DownloadSite(url));
        }
        return results;
    }

    private GaWebDownloadResult DownloadSite(string url)
    {
        using WebClient client = new(); //HttpClient den er anbefalt
        var data = client.DownloadString(url);

        return new GaWebDownloadResult() 
        {
            Size = data.Length,
            Url = url
        };
    }

    #endregion

    #region Asynkron
    // async - await !!
    // async -> return Task, Task<T>
    public async Task<List<GaWebDownloadResult>> StartDownloadSitesAsync()
    {
        return await DownloadSitesAsync();
    }

    private async Task<List<GaWebDownloadResult>> DownloadSitesAsync()
    {
        SetupWebList();
        List<GaWebDownloadResult> results = new();
        foreach (var url in _urls)
        {
            results.Add(await DownloadSiteAsync(url));
        }
        return results;
    }
    private async Task<GaWebDownloadResult> DownloadSiteAsync(string url)
    {
        using HttpClient client = new HttpClient();
        var data = await client.GetStringAsync(url);

        //using WebClient client = new WebClient();
        //var data = await Task.Run(() => client.DownloadString(url));

        return new GaWebDownloadResult()
        {
            Size = data.Length,
            Url = url
        };
    }

    #endregion

    #region Parallell
    public async Task<List<GaWebDownloadResult>> StartDownloadSitesParallellAsync()
    {
        return await StartDownloadSitesParallellAsync();
    }
    private async Task<List<GaWebDownloadResult>> DownloadSitesParallellAsync()
    {
        SetupWebList();

        //liste med tasks
        List<Task<GaWebDownloadResult>> tasks = new();

        List<GaWebDownloadResult> results = new();
        foreach (var url in _urls)
        {
            var t = DownloadSiteAsync(url);
            tasks.Add(t);
            
        }

        // Her venter vi på alle tasker, alle må bli ferdig
        var allTasks = await Task.WhenAll(tasks);

        foreach(var res in allTasks)
        {
            results.Add(res);
        }
        return results;
    }

    #endregion
}