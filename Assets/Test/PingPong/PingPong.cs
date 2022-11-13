using UnityEngine;
public class PingPong
{
    public double x = 0;
    public double y = 0;

    public double xdt = 0;
    public double ydt = 0;

    public void move(double time)
    {
        x += xdt * time;
        y += ydt * time;
    }

    public bool CollisionPingPong(PingPong pingPong, double time)
    {
        if (pingPong == this)
            return false;

        var xDis = Mathf.Abs((float)(x - pingPong.x));
        var yDis = Mathf.Abs((float)(y - pingPong.y));
        if ((xDis + yDis) > 0.3f)
        {
            return false;
        }


        var tmpX = xdt;
        var tmpY = ydt;
        xdt = pingPong.xdt;
        ydt = pingPong.ydt;
        pingPong.xdt = tmpX;
        pingPong.ydt = tmpY;

        return true;
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
