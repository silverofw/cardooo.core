using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PingPongMgr : MonoBehaviour
{
    public double width = 3;
    public double height = 5;
    public float speed = 1f;

    public int amount = 10;

    public GameObject pingpongObj = null;
    PingPong[] pingPongs = null;
    GameObject[] pingPongObjs = null;
    Thread thread;

    // Start is called before the first frame update
    void Start()
    {
        LogicInit();

        pingPongObjs = new GameObject[amount];

        for (int i = 0; i < pingPongs.Length; i++)
        {
            GameObject item = Instantiate(pingpongObj);
            pingPongObjs[i] = item;            
        }


        thread = new Thread(LogicUpdate);
        thread.Start();
    }

    private void OnDestroy()
    {
        if(thread!= null)
            thread.Abort();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < pingPongs.Length; i++)
        {
            PingPong pingPong = pingPongs[i];
            GameObject item = pingPongObjs[i];

            var pos = item.transform.position;
            pos.x = (float)pingPong.x;
            pos.z = (float)pingPong.y;
            item.transform.position = pos;
        }
    }

    void LogicInit()
    {
        pingPongs = new PingPong[amount];

        for (int i = 0; i < pingPongs.Length; i++)
        {
            PingPong item = new PingPong();
            pingPongs[i] = item;
            item.x = Random.Range(0, (float)width);
            item.y = Random.Range(0, (float)height);

            var sp = Random.Range(0, speed);
            item.xdt = sp;
            item.ydt = speed - sp;
        }
    }

    void LogicUpdate()
    {
        var time = 1 / 60d;
        int waitTime = (int)(time * 1000);
        while (true)
        {            
            SpinWait.SpinUntil(() => false, waitTime);

            for (int i = 0; i < pingPongs.Length; i++)
            {
                PingPong pingPong = pingPongs[i];
                
                bool isCollision = false;
                for (int j = 0; j < pingPongs.Length; j++)
                {
                    isCollision = pingPong.CollisionPingPong(pingPongs[j], time, speed);
                }

                if (!isCollision)
                {
                    pingPong.move(time);
                }

                pingPong.CollisionWall(width, height);
            }
        }
    }
}
