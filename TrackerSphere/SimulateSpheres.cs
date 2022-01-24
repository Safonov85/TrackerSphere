using System;
using Cairo;


    public class SimulateSpheres
    {


        public SimulateSpheres()
        {
        }

        public void DrawSphere(Gdk.Window drawingArea)
        {
            using (Cairo.Context circle = Gdk.CairoHelper.Create(drawingArea))
            {
            //Cairo.Context circle = Gdk.CairoHelper.Create(drawingArea);

            int x, y;
            x = 30;
            y = 30;

            circle.Antialias = Antialias.Default;

            circle.Translate(100, 100);
            circle.LineWidth = 4;
            circle.Arc(50, 50, 0, x, y);
            circle.Stroke();

            circle.SetSourceRGB(50, 100, 200);
            circle.Fill();

            circle.GetTarget().Dispose();
            circle.Dispose();
            // Perform some drawing
            }

        }
    }

