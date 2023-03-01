using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.NET;

namespace Vandlent
{
    public class BotStartEvent : Listener
    {
        public override async Task OnBotStartAsync()
        {
            string botUserPage = $"User:{StaticManager.bot.botName}";

            Page page = new Page(botUserPage);

            string toAppend = "<br>The bot has logged on! As of sending this message, it is " + DateTime.Now.ToString() + ".";
            await page.Append(toAppend);

            Console.WriteLine("Append task finished.");
        }
    }
}
