using Discord.WebSocket;

namespace InisBot
{

    record MessageContext(MahCounter MahCounter,  SocketMessage Message, Options Options);

}
