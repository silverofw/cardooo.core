using UnityEngine;
public class PingPong
{
    public float x = 0;
    public float y = 0;

    public float xdt = 0;
    public float ydt = 0;

    public bool isCollision = false;

    public void move(float time)
    {
        x += xdt * time;
        y += ydt * time;
    }

    public void CollisionPingPong(PingPong pingPong, float time, float speed)
    {
        if (isCollision)
            return;

        isCollision = true;
        pingPong.isCollision = true;

        var twoPingVector = new Vector2(pingPong.x - x, pingPong.y - y);

        var forceVector = new Vector2(xdt, ydt);
        float angle = Vector2.Angle(twoPingVector, forceVector);
        var dis = Mathf.Sqrt(Mathf.Pow(xdt, 2) + Mathf.Pow(xdt, 2));
        var targetDis = dis * Mathf.Cos(angle/180 * Mathf.PI);
        var target = twoPingVector.normalized * targetDis;
        var target2 = forceVector - target;

        var twoPingVector2 = new Vector2(x - pingPong.x, y - pingPong.y);
        var forceVector2 = new Vector2(pingPong.xdt, pingPong.ydt);
        float angle2 = Vector2.Angle(twoPingVector2, forceVector2);
        var dis2 = Mathf.Sqrt(Mathf.Pow(pingPong.xdt, 2) + Mathf.Pow(pingPong.xdt, 2));
        var targetDis2 = dis2 * Mathf.Cos(angle2 / 180 * Mathf.PI);
        var target3 = twoPingVector2.normalized * targetDis2;
        var target4 = forceVector2 - target3;


        var newdt = getNewdt(target3, target2, speed);
        xdt = newdt.x;
        ydt = newdt.y;

        newdt = getNewdt(target, target4, speed);
        pingPong.xdt = newdt.x;
        pingPong.ydt = newdt.y;
    }

    public bool canCollision(PingPong pingPong)
    {
        if (pingPong == this)
            return false;

        var xDis = Mathf.Abs((float)(x - pingPong.x));
        var yDis = Mathf.Abs((float)(y - pingPong.y));
        if ((xDis + yDis) > 0.3f)
        {
            return false;
        }
        return true;
    }

    Vector2 getNewdt(Vector2 v1, Vector2 v2, float speed)
    {
        var newdt = v1 + v2;
        newdt = newdt.normalized * speed * 0.6f;
        return newdt;
    }

    public void CollisionWall(float w, float h)
    {
        if (x < 0)
        {
            x = 0;
            xdt *= -1;
        }
        else if (x > w)
        {
            x = w;
            xdt *= -1;
        }

        if (y < 0)
        {
            y = 0;
            ydt *= -1;
        }
        else if (y > h)
        {
            y = h;
            ydt *= -1;
        }
    }
}
