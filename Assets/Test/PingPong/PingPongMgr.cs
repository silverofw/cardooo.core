using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PingPongMgr : MonoBehaviour
{
    public float width = 3;
    public float height = 5;
    public float speed = 1f;

    public int amount = 10;

    public Vector2 ball_1 = Vector2.zero;
    public Vector2 ball_2 = Vector2.zero;

    public Vector2 ball_1_dt = Vector2.zero;
    public Vector2 ball_2_dt = Vector2.zero;


    public GameObject pingpongObj = null;
    PingPong[] pingPongs = new PingPong[0];
    GameObject[] pingPongObjs = new GameObject[0];
    Thread thread;

    private void Start()
    {
        Test1();
    }

    [ContextMenu("Test1")]
    public void Test1()
    {
        OnDestroy();

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


    [ContextMenu("Test2")]
    public void Test2()
    {
        OnDestroy();

        //LogicInit
        pingPongs = new PingPong[2];

        PingPong item = new PingPong();
        pingPongs[0] = item;
        item.x = ball_1.x;
        item.y = ball_1.y;
        
        item.xdt = ball_1_dt.x;
        item.ydt = ball_1_dt.y;

        item = new PingPong();
        pingPongs[1] = item;
        item.x = ball_2.x;
        item.y = ball_2.y;

        item.xdt = ball_2_dt.x;
        item.ydt = ball_2_dt.y;


        //
        pingPongObjs = new GameObject[amount];

        for (int i = 0; i < pingPongs.Length; i++)
        {
            GameObject obj = Instantiate(pingpongObj);
            pingPongObjs[i] = obj;
        }


        thread = new Thread(LogicUpdate);
        thread.Start();
    }

    private void OnDestroy()
    {
        if(thread!= null)
            thread.Abort();

        for (int i = 0; i < pingPongObjs.Length; i++)
        {
            Destroy(pingPongObjs[i]);
        }
        if (thread != null)
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
            pos.x = pingPong.x;
            pos.z = pingPong.y;
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
        var time = 1 / 60f;
        int waitTime = (int)(time * 1000);
        while (true)
        {            
            SpinWait.SpinUntil(() => false, waitTime);

            for (int i = 0; i < pingPongs.Length; i++)
            {
                PingPong pingPong = pingPongs[i];
                
                List<PingPong> collisionList = new List<PingPong>();
                for (int j = 0; j < pingPongs.Length; j++)
                {
                    if (pingPong.canCollision(pingPongs[j]))
                    {
                        collisionList.Add(pingPongs[j]);
                    }
                }


                for (int j = 0; j < collisionList.Count; j++)
                {
                    pingPong.CollisionPingPong(collisionList[j], time, speed);                    
                }

                if (collisionList.Count == 0)
                {
                    pingPong.isCollision = false;
                }

                pingPong.move(time);
                pingPong.CollisionWall(width, height);
            }
        }
    }
}
