using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Collections.Concurrent;

public class InfiniteMonkey : MonoBehaviour
{
    public int MonkeyCount = 1;
    public string Target = "";
    
    Thread[] threads;
    List<int> typeTimes = new List<int>();
    int maxTypeTime;
    int minTypeTime;

    public static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    Action<string> CallBack;

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
        MokeyType(MonkeyCount, Target);
    }


    public void MokeyType(int monkeyCount, string targetString, Action<string> callBack = null)
    {
        if (!check(targetString))
        {
            DLog.LogError($"Have error char!");
            return;
        }

        CallBack = callBack;

        if (threads != null)
        {
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Abort();
            }
            threads = null;
        }

        DLog.Log($"[MokeyType][rate : {Mathf.Pow(list.Count, targetString.Length)}]");

        threads = new Thread[monkeyCount];
        typeTimes.Clear();
        maxTypeTime = int.MinValue;
        minTypeTime = int.MaxValue;

        for (int i = 0; i < monkeyCount; i++)
        {
            threads[i] = new Thread(() => oneMonkeyType(monkeyCount, targetString));
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

    void oneMonkeyType(int monkeyCount, string targetString)
    {        
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        int seed = new System.Random().Next();
        //[seed : 1361853594][abc : 9.3E-05][typeTime: 492]
        //seed = 1361853594;
        System.Random rand = new System.Random(seed);
        
        int typeTime = 0;        
        for (int i = 0, len = targetString.Length; i < len; i += 0)
        {
            char aphba = list[rand.Next(0, list.Count)];
            if (aphba == targetString[i])
            {
                i++;
            }
            else
            {
                i = 0;
            }
            typeTime++;
        }
        
        stopwatch.Stop();
        RunOnMainThread.Enqueue(() => GetTypeCorrectMonkey(
            monkeyCount, targetString, typeTime, stopwatch.Elapsed.TotalSeconds, seed));        
    }

    void GetTypeCorrectMonkey(int monkeyCount, string targetString, int typeTime, double time, int seed)
    {
        DLog.Log($"[seed : {seed}][{targetString} : {time}][typeTime: {typeTime}]");
        typeTimes.Add(typeTime);
        minTypeTime = Mathf.Min(minTypeTime, typeTime);
        maxTypeTime = Mathf.Max(maxTypeTime, typeTime);



        if (typeTimes.Count == monkeyCount)
        {
            int total = 0;
            foreach (var item in typeTimes)
            {
                total += item;
            }

            string log = $"[REPORT][minTypeTime: {minTypeTime}][maxTypeTime: {maxTypeTime}]";
            log += $"\n[REPORT][{(float)total / monkeyCount}/{Mathf.Pow(list.Count, targetString.Length)}]";

            CallBack?.Invoke(log);
            CallBack = null;

            DLog.Log(log);            
        }
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
