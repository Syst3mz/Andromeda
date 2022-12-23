using System;
using System.Threading;
using Silk.NET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Andromeda
{
    class Program
    {


        static void Main(string[] args)
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.Title = "Windowing is real";
            windowOptions.Size = new Vector2D<int>(1280, 720);

            AndromedaWindow window1 = new AndromedaWindow(windowOptions);
            window1.Show();
        }
    }
}