using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Concurrent;

public class InfiniteMonkey : MonoBehaviour
{
    public int monkeyCount = 1;
    public string Target = "";
    
    int[] times;
    Thread[] threads;
    Stopwatch stopwatch;
    System.Random ran;

    public static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    List<char> list = new List<char> {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
        'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z',
    };
    /*
    List<char> list = new List<char> {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
        'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
        'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z',
    };
    */
    [ContextMenu("MokeyType")]
    public void MokeyType()
    {
        if (!check(Target))
        {
            DLog.LogError($"Have error char!");
            return;
        }

        DLog.Log("[MokeyType]");
        stopwatch = new Stopwatch();
        stopwatch.Start();
        
        times = new int[monkeyCount];
        threads = new Thread[monkeyCount];
        ran = new System.Random(7878);
        for (int i = 0; i < monkeyCount; i++)
        {
            threads[i] = new Thread(oneMonkeyType);
            threads[i].Start();
        }
    }

    private void Update()
    {
        if (!RunOnMainThread.IsEmpty)
        {
            while (RunOnMainThread.TryDequeue(out var action))
            {
                action?.Invoke();                
            }
        }
    }

    void oneMonkeyType()
    {
        int times = 0;
        for (int i = 0; i < Target.Length; i += 0)
        {
            System.Random ran = new System.Random();
            var rand = list[ran.Next(0, list.Count)];
            if (rand == Target[i])
            {
                i++;
            }
            else
            {
                i = 0;
            }
            times++;
        }
        
        stopwatch.Stop();
        RunOnMainThread.Enqueue(GetTypeCorrectMonkey);
    }

    void GetTypeCorrectMonkey()
    {        
        int totalTimes = 0;
        if (threads != null)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Abort();
                totalTimes += times[i];
            }
            threads = null;
        }
        DLog.Log($"[{stopwatch.Elapsed.TotalSeconds}][{Target} : {totalTimes}]");
    }

    bool check(string value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (!list.Contains(value[i]))
            {
                return false;
            }
        }

        return true;
    }
}
