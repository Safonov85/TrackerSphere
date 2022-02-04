using System;
using System.Collections.Generic;
using Cairo;
using Gdk;
using Gtk;




public partial class MainWindow : Gtk.Window
{
    SimulateSpheres simSphere = new SimulateSpheres();
    PointD sphere;
    PointD dot;
    double sphereVolume = 50;
    double speed = 20;
    double sphereY;
    double dotVolume = 20;
    double dotRed = 0;
    double dotTransparent = 1;
    List<PointD> dots = new List<PointD>();
    List<double> dotTranspList = new List<double>();
    FollowMouse flwMouse = new FollowMouse();

    DateTime start;

    //KeyPressEvent += KeyPress;

    int widthScreen, heightScreen;


    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        //this.drawingArea.Events = ((global::Gdk.EventMask)(772));

        string mainTitle = "Tracking the Sphere(s)";

        this.Title = mainTitle;

        sphere.X = drawingArea.WidthRequest;
        sphere.Y = GiveRandomNum();

        widthScreen = drawingArea.WidthRequest;
        heightScreen = drawingArea.HeightRequest;

        start = DateTime.UtcNow;


        dot.X = drawingArea.WidthRequest / 2;
        dot.Y = drawingArea.HeightRequest / 2;
        dots.Add(dot);

        dotTranspList.Add(dotTransparent);
        //statusbar.Activate();


        ClockStart();


    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    void ClockStart()
    {
        GLib.Timeout.Add(5, new GLib.TimeoutHandler(Update));
    }


    // Simulation
    bool Update()
    {
        drawingArea.GdkWindow.Clear();

        flwMouse.AddDotsToList(dot, widthScreen, heightScreen);

        for (int i = 0; i < flwMouse.dotsPosition.Count - 1; i++)
        {
            flwMouse.CreateCircleFollow(drawingArea.GdkWindow, flwMouse.dotsPosition[i], 10, flwMouse.dotsTransparent[i]);
        }


        return true;
    }

    void DrawBlackScreen()
    {
        //drawingArea.ModifyBg(StateType.Normal, new Gdk.Color(100, 0, 0));
        //drawingArea.GdkWindow.Clear();

        simSphere.DrawSphere(drawingArea.GdkWindow);
    }

    int GiveRandomNum()
    {
        int num;
        Random rand = new Random();

        num = rand.Next(1, drawingArea.HeightRequest);

        return num;

    }

    int GiveRandomLimited()
    {
        int num = new Random().Next(-5, 6);

        if(dotX < 0 || dotY < 0)
        {
            num = 1;
        }
        if(dotX > widthScreen)
        {
            dotX = widthScreen;
        }
        if(dotY > heightScreen)
        {
            dotY = heightScreen;
        }


        return num;

    }

    void CreateCircle()
    {
        Cairo.Context circle = Gdk.CairoHelper.Create(drawingArea.GdkWindow);

        circle.LineWidth = 0;
        //circle.SetSourceRGB(0.7, 0.2, 0.0);

        int width, height;
        width = Allocation.Width;
        height = Allocation.Height;

        //sphere.X = sphere.X - speed;
        //sphereY = 5 + sphereY;


        circle.Translate(sphere.X, sphere.Y);
        circle.Arc(0, 0, sphereVolume, 0, 2 * Math.PI);
        circle.StrokePreserve();

        circle.SetSourceRGB(0.0, 0.4, 0.6);
        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }

    void CreateDot()
    {
        Cairo.Context circle = Gdk.CairoHelper.Create(drawingArea.GdkWindow);

        circle.LineWidth = 0;



        dotX = GiveRandomLimited() + dotX;
        dotY = GiveRandomLimited() + dotY;


        circle.Translate(dotX, dotY);
        circle.Arc(0, 0, dotVolume, 0, 2 * Math.PI);
        circle.StrokePreserve();


        dotRed = dotRed + 0.01;
        if (dotRed > 1.0)
        {
            dotRed = 0.0;
        }
        circle.SetSourceRGBA(dotRed, 0.4, 0.6, 1.0);


        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }

    void MakeDots()
    {
        if(dotTranspList.Count > 100)
        {
            dotTranspList.RemoveAt(0);
        }
        if(dots.Count > 100)
        {
            dots.RemoveAt(0);
        }

        PointD newDot;
        newDot.X = GiveRandomLimited() + dots[dots.Count - 1].X;
        newDot.Y = GiveRandomLimited() + dots[dots.Count - 1].Y;
        dots.Add(newDot);

        double newDotTransp = dotTranspList[0];
        newDotTransp -= 0.01;
        dotTranspList.Add(newDotTransp);

        for (int i = 0; i < dotTranspList.Count - 1; i++)
        {
            dotTranspList[i] -= 0.01;
        }

        for (int i = 0; i < dots.Count; i++)
        {
            CreateDotFade(i);
        }

    }

    void CreateDotFade(int item)
    {
        Cairo.Context circle = Gdk.CairoHelper.Create(drawingArea.GdkWindow);

        circle.LineWidth = 0;



        //dotX = GiveRandomLimited() + dotX;
        //dotY = GiveRandomLimited() + dotY;


        circle.Translate(dots[item].X, dots[item].Y);
        circle.Arc(0, 0, dotVolume, 0, 2 * Math.PI);
        //circle.StrokePreserve();

        if(dotTransparent < 0.0)
        {
            dotTransparent = 0;
        }

        dotRed = dotRed + 0.01;
        if (dotRed > 1.0)
        {
            dotRed = 0.0;
        }
        circle.SetSourceRGBA(dotRed, 0.4, 0.6, dotTranspList[item]);


        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }

    void MakeGray()
    {
        ImageSurface imageSurf = new ImageSurface(Format.Argb32, widthScreen, heightScreen);
        //Gdk.Pixbuf pixBuf = 

        //Cairo.ImageSurface imgSurface = new Cairo.ImageSurface("/Image.png");
        imageSurf.WriteToPng("/Image.png");
    }


    [GLib.ConnectBefore]
    protected void OnDrawingAreaKeyPressEvent(object o, KeyPressEventArgs args)
    {
        dotRed = 0;
        //args.Event.Key
    }

    protected void OnKeyPressEvent(object o, KeyPressEventArgs args)
    {

    }

    protected void OnKeysChanged(object sender, EventArgs e)
    {

    }

    protected void KeyPress(object sender, KeyPressEventArgs args)
    {

    }

    public double dotX { get; set; }
    public double dotY { get; set; }

    protected void OnDrawingAreaMotionNotifyEvent(object o, MotionNotifyEventArgs args)
    {
        dot.X = args.Event.X;
        dot.Y = args.Event.Y;
    }
}
