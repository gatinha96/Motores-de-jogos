using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    public static class GameConsole
    {
        private static List<Message> messages;
        private static ConsoleMessage consoleMessage;

        static GameConsole()
        {
            messages = new List<Message>(100);
        }

        public static void Update()
        {
            messages = MessageBus.GetMessagesofType(MessageType.Console);
            foreach (Message message in messages)
            {
                consoleMessage = (ConsoleMessage)message;
                Console.WriteLine(consoleMessage.Message);
            }
            messages.Clear();
        }
    }
}