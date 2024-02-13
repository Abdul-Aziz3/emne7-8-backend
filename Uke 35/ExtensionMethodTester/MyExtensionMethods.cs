using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethodTester;

public static class MyExtensionMethods
{
    // bool IsGreaterThan(int testNr)
    public static bool IsGreaterThan(this int x, int testNr)
    {
        return x > testNr;
    }

    public static bool IsEven(this int x) => x % 2 == 0;
    public static int Max(this int x, List<int> list) => list.Max();
    public static int sumNum(this int number)
    {
        int sum = 0;
        int n = Math.Abs(number);
        
        while (n > 0)
        {
            sum += n % 10;
            n /= 10;
        }

        return sum;
    }

    
}
