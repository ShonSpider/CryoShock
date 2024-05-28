using Content.Server.Administration;
using Content.Server.Chat.Systems;
using Content.Shared.Administration;
using Robust.Shared.Console;

namespace Content.Server.Announcements
{
    [AdminCommand(AdminFlags.Admin)]
    public sealed class AnnounceCommand : IConsoleCommand
    {
        public string Command => "announce";
        public string Description => "Send an in-game announcement.";
        public string Help => $"{Command} <sender> <message> or {Command} <message> to send announcement as CentCom.";
        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var chat = IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<ChatSystem>();

            if (args.Length == 0 || args.Length >= 3)
            {
                shell.WriteError("Need one or two arguments!");
                return;
            }

            if (args.Length == 1)
            {
                chat.DispatchAnnouncement(args[0]);
            }
            if (args.Length == 2)
            {
                var message = string.Join(' ', new ArraySegment<string>(args, 1, args.Length - 1));
                chat.DispatchAnnouncement(message, args[0]);
            }
            shell.WriteLine("Sent!");
        }
    }
}
