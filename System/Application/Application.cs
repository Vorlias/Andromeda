﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using Andromeda.System.Utility;
using Andromeda.System.Services;

namespace Andromeda.System
{
    /// <summary>
    /// An application
    /// </summary>
    public class Application
    {
        public class ApplicationSettings
        {
            public bool RescaleViewOnResize = true;
            //public bool CameraIsRelativeToZeroCoord = true;
        }

        RenderWindow window;
        VideoMode mode;
        string title;
        Styles styles;
        bool fullscreen;
        float deltaTime;
        float fps;
        IntPtr context;
        ApplicationSettings settings;
        View gameView, interfaceView;
        HashSet<ThreadedService> services = new HashSet<ThreadedService>();

        public CustomMouseCursor CustomCursor
        {
            get;
        }

        public Service GetService<Service>() where Service : ThreadedService, new()
        {
            var service = services.OfType<Service>();
            if (service.Count() > 0)
            {
                return service.First();
            }
            else
            {
                Service newSvc = new Service();
                newSvc.Start();
                services.Add(newSvc);
                return newSvc;
            }
        }

        public bool IsFullscreen
        {
            get => fullscreen;
        }

        public bool IsFocused
        {
            get => Window.HasFocus();
        }

        public ApplicationSettings Settings
        {
            get => settings;
        }

        internal View WorldView
        {
            get => gameView;
        }

        internal View InterfaceView
        {
            get => interfaceView;
        }

        public void TakeScreenshot(string filename)
        {
            Texture texture = new Texture(window.Size.X, window.Size.Y);
            texture.Update(window);

            texture.CopyToImage().SaveToFile(filename);
        }

        /// <summary>
        /// The delta time of the window
        /// </summary>
        public float DeltaTime
        {
            get
            {
                return deltaTime;
            }
        }

        private float time;

        /// <summary>
        /// The time elapsed since the application started
        /// </summary>
        public float ElapsedTime
        {
            get
            {
                return time;
            }
        }

        /// <summary>
        /// The TextureManager instance
        /// </summary>
        public TextureManager TextureManager => TextureManager.Instance;

        /// <summary>
        /// The FontManager instance
        /// </summary>
        public FontManager FontManager => FontManager.Instance;

        /// <summary>
        /// The SoundManager instance
        /// </summary>
        public SoundManager SoundManager => SoundManager.Instance;

        /// <summary>
        /// The framerate of the window
        /// </summary>
        public float FPS
        {
            get
            {
                return fps;
            }
        }

        /// <summary>
        /// The render window
        /// </summary>
        public RenderWindow Window
        {
            get
            {
                return window;
            }
        }

        /// <summary>
        /// Resize the window
        /// </summary>
        public Vector2u Size
        {
            get
            {
                return window.Size;
            }
            set
            {
                window.Size = value;
            }
        }

        /// <summary>
        /// The position of the centre of the window
        /// </summary>
        public Vector2f WindowCenter
        {
            get
            {
                return new Vector2f(Size.X / 2f, Size.Y / 2f);
            }
        }

        /// <summary>
        /// The VideoMode of the application
        /// </summary>
        public VideoMode VideoMode
        {
            get
            {
                return mode;
            }
        }

        /// <summary>
        /// The title of the Application
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                window.SetTitle(value);
            }
        }
        
        /// <summary>
        /// The mouse's position, relative to the window.
        /// </summary>
        public Vector2i MousePosition
        {
            get => Mouse.GetPosition(Window);
            set => Mouse.SetPosition(value, Window);
        }

        public Styles Styles
        {
            get
            {
                return styles;
            }
        }

        public Application(IntPtr handle)
        {
            context = handle;

            CustomCursor = new CustomMouseCursor();
        }

        /// <summary>
        /// Recreates the window (Used for changing to Fullscreen)
        /// </summary>
        /// <param name="mode">The resolution</param>
        /// <param name="windowStyle">The WindowStyle</param>
        public void RecreateWindow(VideoMode mode, WindowStyle windowStyle)
        {
            RecreateWindow(mode, (Styles)windowStyle);
        }

        /// <summary>
        /// Recreates the window (Used for changing to Fullscreen)
        /// </summary>
        /// <param name="mode">The resolution</param>
        /// <param name="styles">The window styles</param>
        public void RecreateWindow(VideoMode mode, Styles styles)
        {
            this.mode = mode;
            this.styles = styles;
            RenderWindow oldWindow = window;

            

            InitWindow();
            window.SetView(oldWindow.DefaultView);

            oldWindow.Close();
            CustomCursor.Context = window;
            CustomCursor.UpdateVisibility();
            ResizeViews();

            // Will register events
            BeforeStart();
        }

        public Application(VideoMode mode, string title, WindowStyle wndStyle = WindowStyle.Resizable) : this(mode, title, (Styles) wndStyle)
        {
        }

        public Application(VideoMode mode, string title, Styles styles = Styles.Default)
        {
            this.mode = mode;
            this.title = title;
            this.styles = styles;

            CustomCursor = new CustomMouseCursor();
        }
        
        /// <summary>
        /// Method called when the game ends (Window is closed)
        /// </summary>
        protected virtual void End()
        {

        }

        /// <summary>
        /// Method called when the game starts
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// Function called on update
        /// </summary>
        protected virtual void Update()
        {

        }

        /// <summary>
        /// Function called on render
        /// </summary>
        protected virtual void Render()
        {

        }

        protected virtual void BeforeStart()
        {

        }

        protected virtual void InitializeStates()
        {

        }

        protected virtual void AfterStart()
        {

        }

        protected void SetupCursor()
        {
            CustomCursor.Context = window;
            CustomCursor.Texture = null;
        }

        /// <summary>
        /// Initializes the window
        /// </summary>
        protected virtual void InitWindow()
        {
            if (context != default(IntPtr))
            {
                window = new RenderWindow(context);
                window.Closed += WindowClosed;
                window.Resized += Window_Resized;
            }
            else
            {
                window = new RenderWindow(mode, title, styles);
                window.Closed += WindowClosed;
                window.Resized += Window_Resized;
            }


        }

        /// <summary>
        /// Performs the update actions
        /// </summary>
        protected virtual void UpdateEvents()
        {
            window.DispatchEvents();
            Update();
        }

        /// <summary>
        /// Performs all the delta clock updating actions
        /// </summary>
        protected void UpdateDeltaClock()
        {
            deltaTime = deltaClock.ElapsedTime.AsSeconds();
            deltaClock.Restart();
            time += deltaTime;
            fps = 1.0f / deltaTime;
        }

        /// <summary>
        /// Performs all the initialization actions
        /// </summary>
        protected void InitializeApplication()
        {
            InitWindow();
            deltaClock = new Clock();

            SetupCursor();

            BeforeStart();
            Start();
            InitializeStates();
            AfterStart();

            View defaultView = window.DefaultView;
            gameView = new View(defaultView.Center, defaultView.Size);
            interfaceView = new View(defaultView.Center, defaultView.Size);
        }

        Color _clearColor = Color.Black;
        /// <summary>
        /// The clear colour of the application (defaulted to black)
        /// </summary>
        public Color ClearColor
        {
            get => _clearColor;
            set => _clearColor = value;
        }

        /// <summary>
        /// Performs all the rendering actions
        /// </summary>
        protected virtual void UpdateRendering()
        {
            window.Clear(ClearColor);
            Render();
            window.Draw(CustomCursor);
            window.Display();
        }

        Clock deltaClock;

        /// <summary>
        /// Runs the application
        /// </summary>
        public virtual void Run()
        {
            // Run all the stuff to initialize the window
            InitializeApplication();

            while (window.IsOpen)
            {
                // Update stuff like the delta time, time elapsed etc.
                UpdateDeltaClock();

                // Update all the event based stuff
                UpdateEvents();

                // Update all the rendering based stuff
                UpdateRendering();
            }

            End();
            Environment.Exit(0);
        }

       
        protected void ResizeViews()
        {
            gameView = new View(new FloatRect(0, 0, window.Size.X, window.Size.Y));
            interfaceView = new View(new FloatRect(0, 0, window.Size.X, window.Size.Y));
        }

        private void Window_Resized(object sender, SizeEventArgs e)
        {
            var window = sender as RenderWindow;

            gameView = new View(new FloatRect(0, 0, window.Size.X, window.Size.Y));
            interfaceView = new View(new FloatRect(0, 0, window.Size.X, window.Size.Y));
        }

        /// <summary>
        /// Close the application
        /// </summary>
        public void Close()
        {
            window.Close();
        }

        /// <summary>
        /// Called when the window close is requested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
