using GaWebDownloadLibrary;

GaWebDownloader gaWebDownloader = new GaWebDownloader();

Console.WriteLine("Start downloading....");
System.Diagnostics.Stopwatch sw = new();
sw.Start();

//var sites = gaWebDownloader.StartDownloadSites();
//foreach (var site in sites)
//{
//    Console.WriteLine($"Size={site.Size}, Url={site.Url}");
//}

var sites = gaWebDownloader.StartDownloadSitesAsync()
    .ContinueWith(res => 
    {
        foreach (var site in res.Result)
        {
            Console.WriteLine($"Size={site.Size}, Url={site.Url}");
        }
    });


sw.Stop();
Console.WriteLine($"Time: {sw.Elapsed.TotalMilliseconds}");
Console.ReadLine();
