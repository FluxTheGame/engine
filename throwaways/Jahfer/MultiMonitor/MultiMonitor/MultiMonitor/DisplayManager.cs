using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MultiMonitor
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DisplayManager : IGraphicsDeviceService
    {
        private int ScreenCount;
        private Display[] displays;

        // Implement IGraphicsDeviceService
        public GraphicsDevice GraphicsDevice
        {
            get { return displays[0].Graphics; }
        }
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        private ContentManager Content;

        // Construct object
        public DisplayManager(IntPtr windowHandle, GameServiceContainer services)
        {
            ScreenCount = GraphicsAdapter.Adapters.Count;
            displays = new Display[ScreenCount];

            Content = new ContentManager(services);
            Content.RootDirectory = "Content";

            for (UInt16 i=0; i<ScreenCount; i++)
            {
                GraphicsAdapter _adapter = GraphicsAdapter.Adapters[i];
                PresentationParameters settings = new PresentationParameters();
                settings.BackBufferWidth = _adapter.CurrentDisplayMode.Width;
                settings.BackBufferHeight = _adapter.CurrentDisplayMode.Height;
                settings.BackBufferFormat = _adapter.CurrentDisplayMode.Format;
                settings.DeviceWindowHandle = windowHandle;

                displays[i] = new Display(i, Content, _adapter, GraphicsProfile.HiDef, settings);
            }

            services.AddService(typeof(IGraphicsDeviceService), this);
        }

        public void LoadContent()
        {
            foreach (Display m in this.displays)
            {
                m.LoadContent(Content);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Display m in this.displays)
            {
                m.Draw();
            }
        }
    }
}
