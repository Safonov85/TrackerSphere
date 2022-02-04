using System;
using System.Collections.Generic;
using Cairo;

public class FollowMouse
{
    public List<PointD> dotsPosition = new List<PointD>();
    public double[] dotsTransparent = new double[30];

    public FollowMouse()
    {
        double current = 0.0;
        for (int i = 0; i < dotsTransparent.Length - 1; i++)
        {
            current = 0.0333333 + current;
            dotsTransparent[i] = current;
        }
    }

    public void CreateCircleFollow(Gdk.Window drawing, PointD position, double volume, double alpha)
    {
        Cairo.Context circle = Gdk.CairoHelper.Create(drawing);

        circle.LineWidth = 0;
        circle.SetSourceRGB(0.7, 0.2, 0.0);



        //sphere.X = sphere.X - speed;
        //sphereY = 5 + sphereY;


        circle.Translate(position.X, position.Y);
        circle.Arc(0, 0, volume, 0, 2 * Math.PI);
        circle.StrokePreserve();

        circle.SetSourceRGBA(0.9, 0.4, 0.4, alpha);
        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }

    public void AddDotsToList(PointD position, int widthScreen, int heightScreen)
    {
        if(dotsPosition.Count > 30)
        {
            dotsPosition.RemoveAt(0);
        }

        int numberX = GiveRandomLimited(position, widthScreen, heightScreen);
        int numberY = GiveRandomLimited(position, widthScreen, heightScreen);

        position.X = position.X + numberX;
        position.Y = position.Y + numberY;

        dotsPosition.Add(position);
    }

    private int GiveRandomLimited(PointD position, int widthScreen, int heightScreen)
    {
        int num = new Random().Next(-5, 6);

        if (position.X < 0 || position.Y < 0)
        {
            num = 1;
        }
        if (position.X > widthScreen)
        {
            position.X = widthScreen;
        }
        if (position.Y > heightScreen)
        {
            position.Y = heightScreen;
        }

        return num;

    }


}

