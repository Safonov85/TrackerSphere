using System;
using Cairo;
using Gtk;



public partial class MainWindow : Gtk.Window
{
    SimulateSpheres simSphere = new SimulateSpheres();
    PointD sphere;
    PointD dot;
    double sphereVolume = 50;
    double speed = 20;
    double sphereY;
    double dotVolume = 3;
    double dotRed = 0;

    int widthScreen, heightScreen;


    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        string mainTitle = "Tracking the Sphere(s)";

        this.Title = mainTitle;

        sphere.X = drawingArea.WidthRequest;
        sphere.Y = GiveRandomNum();

        widthScreen = drawingArea.WidthRequest;
        heightScreen = drawingArea.HeightRequest;

        

        dotX = drawingArea.WidthRequest / 2;
        dotY = drawingArea.HeightRequest / 2;

        ClockStart();


    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    void ClockStart()
    {
        GLib.Timeout.Add(2, new GLib.TimeoutHandler(Update));
    }


    // Simulation
    bool Update()
    {
        DrawBlackScreen();

        //CreateCircle();
        CreateDot();


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

        sphere.X = sphere.X - speed;
        //sphereY = 5 + sphereY;


        circle.Translate(sphere.X, sphere.Y + sphereY);
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
        if(dotRed > 1.0)
        {
            dotRed = 0.0;
        }
        circle.SetSourceRGB(dotRed, 0.4, 0.6);


        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }

    

    public double dotX { get; set; }
    public double dotY { get; set; }
}
