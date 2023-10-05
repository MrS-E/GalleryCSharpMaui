namespace Gallery
{
    public partial class App : Application
    {
        public static string db_path;
        public App()
        {
            InitializeComponent();

            db_path = Path.Combine(FileSystem.AppDataDirectory, "data.db");

            MainPage = new AppShell();
        }
    }
}