using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace cardooo.core
{
    public class TimeMgr : Singleton<TimeMgr>
    {
        int index = 0;
        List<Timer> dic = new List<Timer>();

        public static float DeltaTime()
        {
            return Time.deltaTime;
        }

        public static float FixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }

        public Timer AddNewTimer(ElapsedEventHandler elapsedEventHandler, double interval = 100)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += new ElapsedEventHandler(elapsedEventHandler);
            timer.Start();

            dic.Add(timer);
            index++;
            return timer;
        }

        public void RemoveTimer(Timer timer)
        {
            if (timer == null)
                return;

            if (dic.Contains(timer))
            {

                timer.Stop();
                timer.Dispose();

                dic.Remove(timer);
            }
        }

        public void Reset()
        {
            foreach (var d in dic)
            {
                d.Stop();
                d.Dispose();
            }
            dic.Clear();
        }
    }
}
