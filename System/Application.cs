using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
using System.Runtime.InteropServices;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{


    /// <summary>
    /// An application
    /// </summary>
    public class Application
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public class ApplicationSettings
        {
            public bool RescaleViewOnResize = true;
            //public bool CameraIsRelativeToZeroCoord = true;
        }

        RenderWindow window;
        VideoMode mode;
        string title;
        Styles styles;
        float deltaTime;
        float fps;
        ApplicationSettings settings;
        View gameView, interfaceView;

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

        /// <summary>
        /// The TextureManager object
        /// </summary>
        public TextureManager TextureManager
        {
            get
            {
                return TextureManager.Instance;
            }
        }

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

        public Styles Styles
        {
            get
            {
                return styles;
            }
        }

        public Application(VideoMode mode, string title, Styles styles = Styles.Default)
        {
            this.mode = mode;
            this.title = title;
            this.styles = styles;
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

        protected virtual void AfterStart()
        {

        }



        /// <summary>
        /// Runs the application
        /// </summary>
        public void Run()
        {


            Clock deltaClock = new Clock();
            window = new RenderWindow(mode, title, styles);
            window.Closed += WindowClosed;
            window.Resized += Window_Resized;

            BeforeStart();
            Start();
            AfterStart();

#if !DEBUG
            ShowWindow(GetConsoleWindow(), SW_HIDE);
#endif

            View defaultView = window.DefaultView;

            gameView = new View(defaultView.Center, defaultView.Size);
            interfaceView = new View(defaultView.Center, defaultView.Size);

            while (window.IsOpen)
            {
                deltaTime = deltaClock.ElapsedTime.AsSeconds();
                deltaClock.Restart();
                
                fps = 1.0f / deltaTime;

                window.Clear();

                window.DispatchEvents();

                Update();

                Render();

                window.Display();
            }

            End();
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
