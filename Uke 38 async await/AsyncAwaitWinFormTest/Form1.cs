using GaWebDownloadLibrary;
using System.Windows.Forms;

namespace AsyncAwaitWinFormTest;

public partial class Form1 : Form
{
    GaWebDownloader _downloader = new GaWebDownloader();
    public Form1()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        var sites = _downloader.StartDownloadSites();
        foreach (var site in sites)
        {
            listBox1.Items.Add($"Size={site.Size}, Url={site.Url}");
        }

        sw.Stop();
        listBox1.Items.Add($"Time: {sw.Elapsed.TotalMilliseconds}");

    }

    private async void button2_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        var sites = await _downloader.StartDownloadSitesAsync();
        foreach (var site in sites)
        {
            listBox1.Items.Add($"Size={site.Size}, Url={site.Url}");
        }

        sw.Stop();
        listBox1.Items.Add($"Time: {sw.Elapsed.TotalMilliseconds}");
    }

    private async void button3_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();

        var sites = await _downloader.StartDownloadSitesParallellAsync();
        foreach (var site in sites)
        {
            listBox1.Items.Add($"Size={site.Size}, Url={site.Url}");
        }

        sw.Stop();
        listBox1.Items.Add($"Time: {sw.Elapsed.TotalMilliseconds}");
    }
}
