using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkLoops;

public class LoopBenchmark
{
    //[Params(100, 1000, 10000)]
    public int size {  get; set; }
    private List<int> items = new();

    //[GlobalSetup]
    public void InitList()
    {
        items = Enumerable.Range(1, size).Select(x => Random.Shared.Next()).ToList();
    }

    //[Benchmark]
    public void For()
    {
        for(int i = 0; i < items.Count; i++)
        {
            var item = items[i];
        }
    }

    //[Benchmark]
    public void While()
    {
        var i= 0;
        while(i < items.Count)
        {
            var item = items[i];
            i++;
        }
    }
}
