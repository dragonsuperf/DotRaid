using System.Collections;
using System.Collections.Generic;

public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(Point other)
    {
        X = other.X;
        Y = other.Y;
    }

    public Point SetWay(int way)
    {
        switch (way)
        {
            case 0: this.Y += 1; return this;
            case 1: this.X += 1; return this;
            case 2: this.Y -= 1; return this;
            case 3: this.X -= 1; return this;
        }
        return null;
    }

    public static int PointToPointWay(Point from, Point to)
    {
        Point result = from - to;
        if (result.X == 0 && result.Y == -1)
        {
            // up
            return 0;
        }
        else if (result.X == 0 && result.Y == 1)
        {

            // down
            return 2;
        }
        else if (result.X == 1 && result.Y == 0)
        {
            // left
            return 3;
        }
        else if(result.X == -1 && result.Y == 0)
        {
            // right
            return 1;
        }
        return -1;
    }

    public static Point WayToPoint(int way)
    {
        if (way < 0 || way > 4) return null;

        switch (way)
        {
            case 0: return new Point(0, 1);
            case 1: return new Point(1, 0);
            case 2: return new Point(0, -1);
            case 3: return new Point(-1, 0);
        }
        return null;
    }

    public static Point operator +(Point a, Point other)
    {
        return new Point(a.X + other.X, a.Y + other.Y);
    }

    public static Point operator -(Point a, Point other)
    {
        return new Point(a.X - other.X, a.Y - other.Y);
    }

    public override string ToString()
    {
        return string.Format("Point x: {0}, y: {1}", this.X, this.Y);
    }
}