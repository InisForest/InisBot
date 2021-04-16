using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InisBot.MessageHandlers
{
    class HelpHandler : IMessageHandler
    {

        private static readonly Regex _match = new Regex("^!help\\b", RegexOptions.IgnoreCase);

        public Task HandleAsync(MessageContext context, Func<Task> next)
        {
            var match = _match.Match(context.Message.Content);
            if (!match.Success)
                return next();

            return context.Message.Channel.SendMessageAsync("**InisBot commands**:\n`!addmah` or `!addmahs 6`    Add one or more määhs manually.\n`!setmahs 60`\t\t\t\t\t\tSet how many times Inis' sheep have määh'd.\n`!getmah` or `!getmahs`\t\tGet how many times Inis' sheep have määh'd.\n`!togglemahconfirms` \t\tToggle whether to post when recording a määh.");
        }

    }
}
