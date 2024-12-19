using System.Text.Json;

namespace TestsForTranslator
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public async Task TranslateFromUKtoEN()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Усім привіт!", "uk", "en");
			Assert.AreEqual(translatedText, "Hello everyone!");
		}

		[TestMethod]
		public async Task TranslateFromUKtoPL()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Всім привіт!", "uk", "pl");
			Assert.AreEqual(translatedText, "Witam wszystkich!");
		}
		[TestMethod]
		public async Task TranslateFromUKtoJA()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Усім привіт!", "uk", "ja");
			Assert.AreEqual(translatedText, "こんにちは、みんな！");
		}
		[TestMethod]
		public async Task TranslateFromENtoUK()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Hello everyone!", "en", "uk");
			Assert.AreEqual(translatedText, "Всім привіт!");
		}
		[TestMethod]
		public async Task TranslateFromENtoDE()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Hello everyone!", "en", "de");
			Assert.AreEqual(translatedText, "Hallo zusammen!");
		}
		[TestMethod]
		public async Task TranslateFromENtoES()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Hello everyone!", "en", "es");
			Assert.AreEqual(translatedText, "¡Hola a todos!");
		}
		[TestMethod]
		public async Task TranslateFromFRtoPT()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Bonjour à tous!", "fr", "pt");
			Assert.AreEqual(translatedText, "Olá pessoal!");
		}
		[TestMethod]
		public async Task TranslateFromPTtoFR()
		{
			var methods = new ImplementedMethods();
			string translatedText = await methods.TranslateTextLingva("Olá pessoal!", "pt", "fr");
			Assert.AreEqual(translatedText, "Salut les gars!");
		}
	}
    public class  ImplementedMethods
    {
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