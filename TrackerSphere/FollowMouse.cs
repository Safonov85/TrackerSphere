using System;
using Cairo;

public class FollowMouse
    {
        public FollowMouse()
        {
        }

    public void CreateCircleFollow(Gdk.Window drawing, PointD position, double volume)
    {
        Cairo.Context circle = Gdk.CairoHelper.Create(drawing);

        circle.LineWidth = 0;
        circle.SetSourceRGB(0.7, 0.2, 0.0);



        //sphere.X = sphere.X - speed;
        //sphereY = 5 + sphereY;


        circle.Translate(position.X, position.Y);
        circle.Arc(0, 0, volume, 0, 2 * Math.PI);
        circle.StrokePreserve();

        circle.SetSourceRGB(0.9, 0.4, 0.4);
        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }
}

