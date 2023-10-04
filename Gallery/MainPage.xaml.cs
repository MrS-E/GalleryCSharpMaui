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

            images = App.ImagesDB.GetImages().ToArray();

            if (images.Length > 0)
            {
                MainImage.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(images[0].uri)));
                BGImage.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(images[0].uri)));
            }
            else
            {
                MainImage.Source = "no_image.png";
                BGImage.Source = "no_image.png";
            }
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
                MainImage.Source = images[count].uri;
                BGImage.Source = images[count].uri;
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
                MainImage.Source = images[count].uri;
                BGImage.Source = images[count].uri;
            }
        }
        void SwipedLeft(object sender, SwipedEventArgs e)
        {
            if (!OverlayLayout.IsVisible && images.Length > 0)
            {
                OverlayLayout.IsVisible = true;
                LabelTitle.Text = images[count].name;
                LabelTag.Text = string.Join(" #", images[count].tags);
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
        void Liked(object sender, EventArgs e)
        {
            Debug.WriteLine("liked");
        }
        async void Added(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new NewPage());
        }
    }
}