#region

using CharacterAI;
using Discord;
using Discord.Gateway;

#endregion

namespace Monitor;

public class Program
{
    private Integration _characterAi;

    private static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
        //Thread.Sleep(-1);
    }

    private async Task MainAsync()
    {
        string token = "edae6eb3e08866e3166de1a1a1878a4ec5f235e7";

        DiscordSocketClient user = new(new DiscordSocketConfig
        {

            Intents = DiscordGatewayIntent.DirectMessages, // | DiscordGatewayIntent.DirectMessageTyping | DiscordGatewayIntent.DirectMessageTyping,
            Cache = true
        });
        user.Login("NjEyNzE4NTE1MDk0NDIxNTA0.GnWx4C.qUYu3Km7r579_pkxdWMyPLdEr3zzIkqbFyxCtY");
        user.OnLoggedIn += Escape;

        _characterAi = new Integration(token);

        // Launch Puppeteer Chrome
        await _characterAi.LaunchChromeAsync();

        // Quick setup
        var setupResult = await _characterAi.SetupAsync("v1-TRNAJ69HKD_z5twRwGf5lZtI7_bpTcjoLrYhsBpM", true);
        if (!setupResult.IsSuccessful)
        {
            // handle error
            Console.WriteLine(setupResult.ErrorReason);
            return;
        }

        // stevo 1003835910930038804
        // me 168178675244597249
        ulong id = 1003835910930038804;
        PrivateChannel privateChannel = await user.CreateDMAsync(id);
        Console.WriteLine(privateChannel.Recipients[0].Username);
        ulong? lastId = privateChannel.LastMessageId;
        List<DiscordMessage> messages = new();
        DiscordMessage msg;
        string reply;
        while (true)
        {
            //incredibly autistic solution, but it works

            privateChannel = await user.CreateDMAsync(id);
            // ensure that we didn't send the message
            messages = (List<DiscordMessage>)await privateChannel.GetMessagesAsync(); //.ToList();
            //messages.RemoveAll(x => x == null);
            //msg = messages.Find(x => x.ReferencedMessage.Id == lastId);
            bool send = messages.First().Author.User.Id == user.User.Id;
            //Console.WriteLine($"are we the same nigga: {send}. our id: {user.User.Id}, his id: {messages.First().Author.User.Id}. number of messages: {messages.Count}");
            if (lastId != privateChannel.LastMessageId && send == false) //privateCzhannel.LastMessageId)
            {
                lastId = privateChannel.LastMessageId;
                Console.WriteLine("New message detected. deferring to AI.");
                var characterResponse = await _characterAi.CallCharacterAsync(messages.First().Content);
                if (!characterResponse.IsSuccessful)
                {
                    // handle error
                    Console.WriteLine(characterResponse.ErrorReason);
                    return;
                }

                reply = characterResponse.Response.Text;
                await privateChannel.SendMessageAsync(new MessageProperties
                {
                    Content = reply,
                    ReplyTo = messages.First().MessageReference
                });
            }
            // keep the program running
        }
    }

    public async void Escape(DiscordSocketClient user, LoginEventArgs e)
    {
        Console.WriteLine("Logged in, now listening for messages.");
        user.SubscribeToGuildEvents(1113990851006316565);
    }

    public async void BeBasedAsHell(DiscordSocketClient a, MessageEventArgs e)
    {
        Console.WriteLine(e.Message.Content);
        /*Console.WriteLine(e.Message.Content);
        // Sending message
        var characterResponse = await _characterAi.CallCharacterAsync(e.Message.Content);

        if (!characterResponse.IsSuccessful)
        {
            // handle error
            Console.WriteLine(characterResponse.ErrorReason);
            return;
        }

        string reply = characterResponse.Response.Text; //.First().Text;
        Console.WriteLine(reply);*/
    }
}

/*_characterAi = new Integration(token);

// Launch Puppeteer Chrome
await _characterAi.LaunchChromeAsync();

// Quick setup
var setupResult = await _characterAi.SetupAsync(characterId: "OYYf4iM6fjt9eZ72oXRsY3UGPeXd9Y-uJwfAjF5JAwk", startWithNewChat: true);

if (!setupResult.IsSuccessful)
{
    // handle error
    Console.WriteLine(setupResult.ErrorReason);
    return;
}*/
/*PrivateChannel chaaanel = user.CreateDM(612718515094421504);
Console.WriteLine($"channel name {chaaanel.Name}");
Console.WriteLine($" last message id {chaaanel.LastMessageId}");
ulong? lastId = chaaanel.LastMessageId;
for (;;)
{
    Console.WriteLine("fa");
    if (chaaanel.LastMessageId != lastId)
    {
        lastId = chaaanel.LastMessageId;
        Console.WriteLine("Sup nigga.");
    }

    chaaanel.Update();
    user.SendMessageAsync(1117805130285326399, Console.ReadLine());
*/