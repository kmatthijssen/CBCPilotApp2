using System;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CBCPilotApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            this.InitializeComponent();
        }

        public async Task<Boolean> PickFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".xlsx");
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".xml");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.PickedFile.Text = "Picked file: " + file.Path + file.Name;

                SetStatusImage(StatusFileUpload, true);
                return true;
            }
            else
            {
                this.PickedFile.Text = "No File Accessed.";
                SetStatusImage(StatusFileUpload, false);
                return false;
            }
        }

        private void SetStatusImage(Image sender, bool Success)
        {
            Image img = sender as Image;
            BitmapImage bitmapImage = new BitmapImage();
            img.Width = bitmapImage.DecodePixelWidth = 80; //natural px width of image source
                                                           // don't need to set Height, system maintains aspect ratio, and calculates the other
                                                           // dimension, so long as one dimension measurement is provided
            if (Success == true)
            {
                bitmapImage.UriSource = new Uri(img.BaseUri, "/Images/greencheck.png");
            }
            else
            {
                bitmapImage.UriSource = new Uri(img.BaseUri, "/Images/redcross.png");
            }



            sender.Source = bitmapImage;
            sender.Width = 25;
            sender.Height = 26;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //comment to push changed min version
            Task result = PickFile();
        }
        private void ButtonCheckInternet_Click(object sender, RoutedEventArgs e)
        {
            //comment to push changed min version
            Task result = CheckInternet();
        }




        public async Task CheckInternet()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            bool HasInternetAccess = (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            if (HasInternetAccess)
            {
                textblock_internet.Text = "internet connectivity";
            }
            else
            {
                textblock_internet.Text = "no internet connectivity";
            }

            SetStatusImage(StatusInternet, HasInternetAccess);

        }








    }
}
