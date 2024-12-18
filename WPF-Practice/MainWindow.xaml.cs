using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Practice
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			sourceLandBox.SelectedIndex = 0;
			targetLandBox.SelectedIndex = 1;
		}
		private async void TranslateButton_Click(object sender, RoutedEventArgs e)
		{
			string inputText = textIn.Text;
			if (string.IsNullOrWhiteSpace(inputText))
			{
				MessageBox.Show("Please enter text to translate.");
				return;
			}

			string sourceLanguage = ((ComboBoxItem)sourceLandBox.SelectedItem).Tag.ToString();
			string targetLanguage = ((ComboBoxItem)targetLandBox.SelectedItem).Tag.ToString();

			string translatedText = await TranslateTextLingva(inputText, sourceLanguage, targetLanguage);
			textOut.Text = translatedText;

		}

		public async Task<string> TranslateTextLingva(string text, string sourceLang, string targetLang)
		{
			string baseUrl = "https://lingva.ml/api/v1";
			string url = $"{baseUrl}/{sourceLang}/{targetLang}/{Uri.EscapeDataString(text)}";

			using HttpClient client = new HttpClient();
			var response = await client.GetAsync(url);

			string jsonResponse = await response.Content.ReadAsStringAsync();
			var result = JsonDocument.Parse(jsonResponse);
			return result.RootElement.GetProperty("translation").GetString();


		}
	}


}