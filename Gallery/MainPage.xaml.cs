using Gallery.Data;
using System.Diagnostics;

namespace Gallery
{
    public partial class MainPage : ContentPage
    {

        Models.Image[] images;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            SetupShaker();

            images = App.ImagesDB.GetImages().ToArray();

            if (images.Length > 0)
            {
                LoadImg(images[0]);
            }
            else
            {
                MainImage.Source = "no_image.png";
                BGImage.Source = "no_image.png";
            }
        }

        private void LoadImg(Models.Image image)
        {
            MainImage.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(image.uri)));
            BGImage.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(image.uri)));
        }

        void SwipedDown(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("down");
            if (!OverlayLayout.IsVisible && images.Length>0)
            {
                if (count < images.Length - 1)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
                LoadImg(images[count]);
            }
        }
        void SwipedUp(object sender, SwipedEventArgs e)
        {
            Debug.WriteLine("up");
            if (!OverlayLayout.IsVisible && images.Length > 0)
            {
                if (count > 0)
                {
                    count--;
                }
                else
                {
                    count = images.Length - 1;
                }
                LoadImg(images[count]);
            }
        }
        void SwipedLeft(object sender, SwipedEventArgs e)
        {
            if (!OverlayLayout.IsVisible && images.Length > 0)
            {
                OverlayLayout.IsVisible = true;
                LabelTitle.Text = images[count].name;
                LabelTag.Text = string.Join(" #", images[count].tags.Split(';'));
                LabelDesc.Text = images[count].desc;
                LabelCreated.Text = images[count].createdAt;
            }
        }
        void SwipedRight(object sender, SwipedEventArgs e)
        {
            if (OverlayLayout.IsVisible)
            {
                OverlayLayout.IsVisible = false;
            }
        }
        async void Liked(object sender, EventArgs e)
        {
            Debug.WriteLine("liked");
            await DisplayAlert("Liked", "Thx for the like.", "OK");

        }
        async void Added(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NewPage());
        }
        void Shaken(object sender, EventArgs e)
        {
            if (!OverlayLayout.IsVisible && images.Length > 0)
            {
                if (count > 0)
                {
                    count--;
                }
                else
                {
                    count = images.Length - 1;
                }
                LoadImg(images[count]);
            }
        }

        private async void SetupShaker()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Sensors>();
            if (status != PermissionStatus.Granted)
            {
                // Request accelerometer permission
                status = await Permissions.RequestAsync<Permissions.Sensors>();
                if (status != PermissionStatus.Granted)
                {
                    return;
                }
            }

            if (!Accelerometer.IsMonitoring)
            {
                Accelerometer.ShakeDetected += Shaken;
                Accelerometer.Start(SensorSpeed.Default);
            }
        }
    }
}