using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Speech.Synthesis;
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
			sourceLangBox.SelectedIndex = 1;
			targetLangBox.SelectedIndex = 0;
		}
		private async void TranslateButton_Click(object sender, RoutedEventArgs e)
		{
			string inputText = textIn.Text;
			if (string.IsNullOrWhiteSpace(inputText))
			{
				MessageBox.Show("Please enter text to translate.");
				return;
			}

			string sourceLanguage = ((ComboBoxItem)sourceLangBox.SelectedItem).Tag.ToString();
			string targetLanguage = ((ComboBoxItem)targetLangBox.SelectedItem).Tag.ToString();

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

		public void SpeakText(string text, string language)
		{
			using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
			{
				var voices = synthesizer.GetInstalledVoices().Select(v => v.VoiceInfo).Where(c => c.Culture.Name.StartsWith(language));
				if (voices.Any())
				{
					synthesizer.SelectVoice(voices.First().Name);
				}
				
				synthesizer.Speak(text);
			}
		}

		private void soundButtonIn_Click(object sender, RoutedEventArgs e)
		{
			string text = textIn.Text;
			string language = ((ComboBoxItem)sourceLangBox.SelectedItem).Tag.ToString();

			SpeakText(text, language);
		}

		private void soundButtonOut_Click(object sender, RoutedEventArgs e)
		{
			string text = textOut.Text;
			string language = ((ComboBoxItem)targetLangBox.SelectedItem).Tag.ToString();

			SpeakText(text, language);
		}

		private void sourceLangBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			textIn.Text = "";
		}

		private void targetLangBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			textOut.Text = "";
		}
	}


}