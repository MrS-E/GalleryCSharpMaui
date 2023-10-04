using Gallery.Data;

namespace Gallery
{
    public partial class App : Application
    {
        public static ImagesDB ImagesDB { get; set; }
        public App(ImagesDB imgDB)
        {
            InitializeComponent();
            
            ImagesDB = imgDB;

            MainPage = new AppShell();
        }
    }
}