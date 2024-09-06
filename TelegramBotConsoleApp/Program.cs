using System.Data.Common;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotConsoleApp
{
	internal class Program
	{
		static TelegramBotClient Bot = new TelegramBotClient("Bot-Token");
		static HttpClient httpClient = new HttpClient();
		static async Task Main(string[] args)
		{

			using var cts = new CancellationTokenSource();

			Bot.StartReceiving(
				HandleUpdate,
				HandleErrorAsync,
				new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
				cancellationToken: cts.Token
				);
			Console.WriteLine("Bot");
			await Task.Delay(-1);
		}

		public static async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
			{
				var chatId = update.Message.Chat.Id;
				var messageText = update.Message.Text;

				Console.WriteLine($"Sending message text for User {update.Message.Chat.Username}:{messageText}");

				await botClient.SendTextMessageAsync(
					chatId: chatId,
					text: "Salam aleykum " + update.Message.Chat.Username,
					cancellationToken: cancellationToken
				);
			}
			//if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Audio)
			//{
			//	var chatId = update.Message.Chat.Id;
			//	var messageAudio = update.Message.Audio;

			//	Console.WriteLine($"Sending audio message for User {update.Message.Chat.Username}:{messageAudio}");

			//	var audioUrl = "https://telegrambots.github.io/book/docs/audio-guitar.mp3";
			//	var fileStream = await httpClient.GetStreamAsync(audioUrl);

			//	await botClient.SendAudioAsync(
			//		chatId: chatId,
			//		audio: new InputFile(fileStream, "audio-guitar.mp3"),
			//		cancellationToken: cancellationToken
			//	);
			//}
		}

		public static void OnUpdate(UpdatedEventArgs e)
		{
            Console.WriteLine("hi, hello group");
		}
		public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Error: {exception.Message}");
			return Task.CompletedTask;
		}


	}
}
