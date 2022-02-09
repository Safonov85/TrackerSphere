using System;
using Cairo;
using Gdk;
using Gtk;
using GLib;

public class BlurImage
{
    public BlurImage()
    {

    }

    void DrawImage(PointD position, Gdk.Window drawing)
    {

        Cairo.Context image = Gdk.CairoHelper.Create(drawing);

        image.LineWidth = 0;


        image.Translate(position.X, position.Y);
        

        image.StrokePreserve();

        image.SetSourceRGBA(0.9, 0.4, 0.4, 1.0);
        image.Fill();

        image.GetTarget().Dispose();
        ((IDisposable)image).Dispose();

    }

    public void OpenImageFile()
    {
        var buff = System.IO.File.ReadAllBytes("starfruit.jpg");

        Gdk.Pixbuf display;

        //display = new Gdk.Pixbuf("starfruit.jpg");

        
            //Rsvg.Pixbuf.FromFile(args[0]);
    }
}

