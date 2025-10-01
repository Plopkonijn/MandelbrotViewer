using System.Numerics;

namespace Viewer;

internal sealed partial class Form1 : Form
{
    private const int MaximumIterations = 1000;
    private const double MaximumMagnitude = 2;

    public Form1()
    {
        InitializeComponent();
    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(Color.White);
        using var bitmap = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
        for (var xPixel = e.ClipRectangle.Left; xPixel < e.ClipRectangle.Right; xPixel++)
        {
            for (var yPixel = e.ClipRectangle.Top; yPixel < e.ClipRectangle.Bottom; yPixel++)
            {
                // set xCoordinate and yCoordinate to be in the range of -2 to 2
                var xCoordinate = xPixel / (float)pictureBox1.Width * 4 - 2;
                var yCoordinate = yPixel / (float)pictureBox1.Height * 4 - 2;
                var iterations = CalculateMandelbrotIterations(xCoordinate, yCoordinate);

                var color = iterations % 2 == 0 ? Color.Black : Color.White;
                bitmap.SetPixel(xPixel - e.ClipRectangle.Left, yPixel - e.ClipRectangle.Top, color);
            }
        }

        e.Graphics.DrawImage(bitmap, e.ClipRectangle);
    }

    private static int CalculateMandelbrotIterations(double xCoordinate, double yCoordinate)
    {
        var c = new Complex(xCoordinate, yCoordinate);
        var z = Complex.Zero;
        var iterations = 0;
        while (z.Magnitude < MaximumMagnitude && iterations < MaximumIterations)
        {
            z = z * z + c;
            iterations++;
        }

        return iterations;
    }
}