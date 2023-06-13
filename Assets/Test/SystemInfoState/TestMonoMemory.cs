using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TestMonoMemory : MonoBehaviour
{
    void Do()
    {
        CallBack01();
        CallBack02();
        CallBack03();
    }

    public void CallBack01()
    {
        List<int> i = new List<int>(1024 * 1024 * 100 / 4);
        //CallBack02();
    }

    public void CallBack02()
    {
        List<int> i = new List<int>(1024 * 1024 * 100 / 4);
        //CallBack03();
    }

    public void CallBack03()
    {
        List<int> i = new List<int>(1024 * 1024 * 100 / 4);
    }

    private void OnGUI()
    {
        GUILayout.Label("Allocated Mono heap size :" + Profiler.GetMonoHeapSizeLong() / (1024 * 1024) + "MB");
        GUILayout.Label("Mono used size :" + Profiler.GetMonoUsedSizeLong() / (1024 * 1024) + "MB");

        GUILayout.Label("Total Reserved memory by Unity: " + Profiler.GetTotalReservedMemoryLong() / (1024 * 1024) + "MB");
        GUILayout.Label("- Allocated memory by Unity: " + Profiler.GetTotalAllocatedMemoryLong() / (1024 * 1024) + "MB");
        GUILayout.Label("- Reserved but not allocated: " + Profiler.GetTotalUnusedReservedMemoryLong() / (1024 * 1024) + "MB");

        if (GUILayout.Button("DO"))
        {
            Do();
        }

        if (GUILayout.Button("DO1"))
        {
            CallBack01();
        }

        if (GUILayout.Button("DO2"))
        {
            CallBack02();
        }

        if (GUILayout.Button("DO3"))
        {
            CallBack03();
        }

        if (GUILayout.Button("GC"))
        {
            System.GC.Collect();
        }
    }
}
