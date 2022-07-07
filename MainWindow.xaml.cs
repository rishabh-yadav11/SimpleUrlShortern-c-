using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleUrlShortern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private async Task CallTheUrl(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://tinyurl.com/api-create.php?url=" + url),

            };




            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Clipboard.SetText(body);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                string url = Clipboard.GetText(TextDataFormat.Text);


                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    _ = CallTheUrl(url);
                    MessageBox.Show("Url Has been Copied To your ClipBoard");
                }
                else
                {
                    MessageBox.Show("Not a valid url");

                }

            }
            else
            {
                MessageBox.Show("Copy a url first");
            }
        }
    }
}
