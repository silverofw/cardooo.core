using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class MyBurst2Behavior : MonoBehaviour
{
    public InputField field;

    public Text log;

    [ContextMenu("Caclulate Burst")]
    public void CaclulateBurst()
    {
        int index = int.Parse(field.text);

        var input = new NativeArray<float>(index, Allocator.TempJob);
        var output = new NativeArray<float>(1, Allocator.Persistent);
        for (int i = 0; i < input.Length; i++)
            input[i] = 1.0f * i;

        var job = new MyJob
        {
            Input = input,
            Output = output
        };

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        job.Schedule().Complete();

        stopwatch.Stop();

        log.text = $"[BURST][Stopwatch: {stopwatch.Elapsed}][Ans: {output[0]}]";
        input.Dispose();
        output.Dispose();
    }

    [ContextMenu("Caclulate")]
    public void Caclulate()
    {
        int index = int.Parse(field.text);
        //log.text = TestFibonacci.Test(int.Parse(field.text));

        var input = new float[index];
        for (int i = 0; i < input.Length; i++)
            input[i] = i;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        float result = 0.0f;
        for (int i = 0; i < index; i++)
        {
            result += input[i];
        }

        stopwatch.Stop();
        log.text = $"[Stopwatch: {stopwatch.Elapsed}][Ans: {result}]";
    }

    // Using BurstCompile to compile a Job with Burst
    // Set CompileSynchronously to true to make sure that the method will not be compiled asynchronously
    // but on the first schedule
    [BurstCompile(CompileSynchronously = true)]
    private struct MyJob : IJob
    {
        [ReadOnly]
        public NativeArray<float> Input;

        [WriteOnly]
        public NativeArray<float> Output;

        public void Execute()
        {            
            float result = 0.0f;
            for (int i = 0; i < Input.Length; i++)
            {
                result += Input[i];
            }
            Output[0] = result;            

            //Output[0] = Fibonacci(Input.Length);
        }

        int Fibonacci(int index)
        {
            if (index == 1 || index == 2)
            {
                return 1;
            }

            return Fibonacci(index - 1) + Fibonacci(index - 2);
        }
    }
}