using System.Diagnostics;
using UnityEngine;

public class TestFibonacci : MonoBehaviour
{
    public int index;

    [ContextMenu("Caclulate")]    
    void Caclulate()
    {
        double ans;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        ans = Fibonacci(index);
        stopwatch.Stop();
        DLog.Log($"[Fibonacci][Stopwatch: {stopwatch.Elapsed}][ans: {ans}]");

        stopwatch = new Stopwatch();
        stopwatch.Start();
        ans = Fibonacci2(index);
        stopwatch.Stop();
        DLog.Log($"[Fibonacc2][Stopwatch: {stopwatch.Elapsed}][ans: {ans}]");
    }

    static double Fibonacci(int index)
    {
        if (index == 1 || index == 2)
        {
            return 1;
        }

        return Fibonacci(index - 1) + Fibonacci(index - 2);
    }

    double Fibonacci2(int index)
    {
        int[] fibs = new int[index + 1];
        fibs[1] = 1;
        fibs[2] = 1;
        for (int i = 3; i <= index; i++)
        {
            fibs[i] = fibs[i - 1] + fibs[i - 2];
        }

        return fibs[index];
    }

    public static string Test(int index)
    {
        double ans;
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        ans = Fibonacci(index);
        stopwatch.Stop();
        return $"[Fibonacci][Stopwatch: {stopwatch.Elapsed}][Ans: {ans}]";
    }
}
