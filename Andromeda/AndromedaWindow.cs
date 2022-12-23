using System.Collections.Generic;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using Silk.NET.Windowing;
using SkiaSharp;

namespace Andromeda
{
    public class AndromedaWindow
    {
        // Silk.Net Stuff
        private IWindow _window;
        private IInputContext _input;
        private GL _gl;
        
        // skia needs this to interface with opengl
        private GRGlInterface _skiaGlInterface;
        private GRContext _skiaBackendContext;
        private GRBackendRenderTarget _skiaRenderTarget;
        private SKColorType _colorFormat;
        
        // basic skia stuff
        private SKSurface _surface;
        private SKCanvas _canvas;
        private SKPaint _paint;
        
        // Andromeda
        private ViewTree _vt;

        public AndromedaWindow(WindowOptions w)
        {
            _window = Window.Create(w);

            _window.Load += OnWindowOnLoad;
            _window.Update += (d) => { };
            _window.Render += OnWindowOnRender;
            _window.Resize += WindowOnResize;
        }

        public void Show()
        {
            _window.Run();
        }
        
        private void OnWindowOnRender(double dt)
        {
            _vt.Draw();
        }

        private void OnWindowOnLoad()
        {
            _skiaGlInterface = GRGlInterface.Create(name =>
            {
                if (_window.GLContext.TryGetProcAddress(name, out nint fn))
                {
                    return fn;
                }

                return (nint)0;
            });

            _skiaBackendContext = GRContext.CreateGl(_skiaGlInterface);

            _colorFormat = SKColorType.Rgba8888;

            _skiaRenderTarget = new GRBackendRenderTarget(
                _window.Size.X, _window.Size.Y,
                _window.Samples ?? 1, _window.PreferredStencilBufferBits ?? 16, 
                new GRGlFramebufferInfo(
                    0,
                    _colorFormat.ToGlSizedFormat()
                ));

            _surface = SKSurface.Create(_skiaBackendContext, _skiaRenderTarget, _colorFormat);
            _canvas = _surface.Canvas;
            _paint = new SKPaint();

            _vt = new ViewTree(_canvas, _paint);

            _paint.Color = SKColors.DarkRed;
            _vt.Root = new Stack(StackDirection.Vertical, new AbsoluteLayout(new Vector2(0, 0)), null,
                new List<IViewWidget>()
                {
                    new Frame(SKColors.White, 20, null, new JustABox(SKColors.Red, null, Vector2.One * 100)),
                    new Frame(SKColors.White, 20, null, new JustABox(SKColors.Green, null, Vector2.One * 100)),
                    new Frame(SKColors.White, 20, null, new JustABox(SKColors.Blue, null, Vector2.One * 100)),
                });
        }

        private void WindowOnResize(Vector2D<int> newSize)
        {
            Logger.Log(newSize.ToString());
        }
    }
}