using System;
using Cairo;
using Gdk;
using Gtk;
using GLib;


public class BlurImage : Blur
{
    Context image;
    ImageSurface surfImage;

    public BlurImage()
    {

    }

    public void DrawImage(PointD position, Gdk.Window drawing, double alpha)
    {

        image = Gdk.CairoHelper.Create(drawing);


        image.Translate(position.X, position.Y);
        //image.Fill();
        //image.Source = new Pattern(image.con)
        //image.Source = new Pattern(surfImage);
        //image.SetSourceRGBA(0.9, 0.4, 0.4, 0.3);
        //image.SetSourceColor(new Cairo.Color(0.5, 0.5, 0.5, 0.4));

        image.SetSource(new SurfacePattern(surfImage));

        image.PaintWithAlpha(alpha);
        //image.set


        image.GetTarget().Dispose();
        ((IDisposable)image).Dispose();

    }


    // for opening picture
    public void OpenImageFile()
    {
        //var buff = System.IO.File.ReadAllBytes("starfruit.jpg");

        //Gdk.Pixbuf display;

        // Works ONLY on png picture files
        surfImage = new ImageSurface("blue_ball_dots.png");
        //surfImage.CreateSimilar(Content.Alpha, surfImage.Width, surfImage.Height);
        

        //surfImage.WriteToPng("lal.png");
        //surfImage.Data.Clone();


        image = new Context(surfImage);

        //display = new Gdk.Pixbuf("starfruit.jpg");


        //Rsvg.Pixbuf.FromFile(args[0]);
    }
}

