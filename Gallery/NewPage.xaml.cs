using Gallery.Data;
using System.Diagnostics;

namespace Gallery;

public partial class NewPage : ContentPage
{
    private string base64;
    public NewPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        Debug.WriteLine("Back");
        App.Current.MainPage = new NavigationPage(new MainPage());
        return true;
    }

    async void SelectedFileClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select a file"
            });

            if (result != null)
            {
                var memoryStream = new MemoryStream();
                await (await result.OpenReadAsync()).CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();
                var base64String = Convert.ToBase64String(fileBytes);

                ImageDiesplay.Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(base64String)));
                base64 = base64String;
                Debug.WriteLine(base64String);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    async void Upladed(object sender, EventArgs e)
    {
        if (base64 != null)
        {
            ImagesDB d = new ImagesDB(App.db_path);
            d.Init();
            d.Add(new Models.Image
            {
                uri = base64,
                name = EName.Text != null ? EName.Text : "",
                tags = ETags.Text != null ? string.Join(';', ETags.Text.Replace(" ", "").Split('#')) : "",
                desc = EDesc.Text != null ? EDesc.Text : "",
                createdAt = DateTime.Today.ToString()

            });
            d.Close();
            App.Current.MainPage = new NavigationPage(new MainPage());

        }
        else
        {
            await DisplayAlert("No Image dedected", "Please upload a image", "OK");
        }
    }

}