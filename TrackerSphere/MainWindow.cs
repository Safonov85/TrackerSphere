using System;
using Cairo;
using Gtk;


public partial class MainWindow : Gtk.Window
{
    SimulateSpheres simSphere = new SimulateSpheres();
    PointD sphere;
    double sphereVolume = 50;
    double speed = 20;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        string mainTitle = "Tracking the Sphere(s)";

        this.Title = mainTitle;

        sphere.X = drawingArea.WidthRequest;
        sphere.Y = GiveRandomNum();

        ClockStart();


    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    void ClockStart()
    {
        GLib.Timeout.Add(9, new GLib.TimeoutHandler(Update));
    }


    // Simulation
    bool Update()
    {
        DrawBlackScreen();

        CreateCircle();



        return true;
    }

    void DrawBlackScreen()
    {
        //drawingArea.ModifyBg(StateType.Normal, new Gdk.Color(100, 0, 0));
        drawingArea.GdkWindow.Clear();

        simSphere.DrawSphere(drawingArea.GdkWindow);
    }

    int GiveRandomNum()
    {
        int num;
        Random rand = new Random();

        num = rand.Next(1, drawingArea.HeightRequest);

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



        circle.Translate(sphere.X, sphere.Y);
        circle.Arc(0, 0, sphereVolume, 0, 2 * Math.PI);
        circle.StrokePreserve();

        circle.SetSourceRGB(0.3, 0.4, 0.6);
        circle.Fill();

        circle.GetTarget().Dispose();
        ((IDisposable)circle).Dispose();
    }
}
