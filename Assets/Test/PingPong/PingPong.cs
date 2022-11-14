using UnityEngine;
public class PingPong
{
    public double x = 0;
    public double y = 0;

    public double xdt = 0;
    public double ydt = 0;

    public PingPong lastCollision = null;

    public void move(double time)
    {
        x += xdt * time;
        y += ydt * time;
    }

    public bool CollisionPingPong(PingPong pingPong, double time, float speed)
    {
        if (lastCollision == pingPong)
            return false;

        if (pingPong == this)
            return false;

        var xDis = Mathf.Abs((float)(x - pingPong.x));
        var yDis = Mathf.Abs((float)(y - pingPong.y));
        if ((xDis + yDis) > 0.3f)
        {
            return false;
        }

        lastCollision = pingPong;

        var twoPingVector = new Vector2((float)(pingPong.x - x), (float)(pingPong.y - y));

        var forceVector = new Vector2((float)xdt, (float)ydt);
        float angle = Vector2.Angle(twoPingVector, forceVector);
        var dis = Mathf.Sqrt(Mathf.Pow((float)xdt, 2) + Mathf.Pow((float)xdt, 2));
        var targetDis = dis * Mathf.Cos(angle/180 * Mathf.PI);
        var target = twoPingVector.normalized * targetDis;
        var target2 = forceVector - target;

        var twoPingVector2 = new Vector2((float)(x - pingPong.x), (float)(y - pingPong.y));
        var forceVector2 = new Vector2((float)pingPong.xdt, (float)pingPong.ydt);
        float angle2 = Vector2.Angle(twoPingVector2, forceVector2);
        var dis2 = Mathf.Sqrt(Mathf.Pow((float)pingPong.xdt, 2) + Mathf.Pow((float)pingPong.xdt, 2));
        var targetDis2 = dis2 * Mathf.Cos(angle2 / 180 * Mathf.PI);
        var target3 = twoPingVector2.normalized * targetDis2;
        var target4 = forceVector2 - target3;


        var newdt = getNewdt(target3, target2, speed);
        xdt = newdt.x;
        ydt = newdt.y;

        newdt = getNewdt(target, target4, speed);
        pingPong.xdt = newdt.x;
        pingPong.ydt = newdt.y;

        //move(time * 2);

        return true;
    }

    Vector2 getNewdt(Vector2 v1, Vector2 v2, float speed)
    {
        var newdt = v1 + v2;
        newdt = newdt.normalized * speed;
        return newdt;
    }

    public void CollisionWall(double w, double h)
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
