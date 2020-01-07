//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    static public class MessageBus
    {
        private static List<Message> tempmessages;
        static public List<Message> TempMessages
        {
            get { return tempmessages; }
            set { tempmessages = value; }
        }

        private static List<Message> messages;
        static public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public static void Initialize()
        {
            messages = new List<Message>();
            tempmessages = new List<Message>();
        }

        public static void InsertNewMessage(Message message)
        {
            messages.Add(message);
        }

        public static List<Message> GetMessagesofType(MessageType messageType)
        {
            tempmessages.Clear();
            foreach(Message m in messages)
            {
                if(m.MessageType == MessageType.Console)
                {
                    tempmessages.Add(m);
                }
            }

            //Clear collected messages
            foreach(Message message in tempmessages)
            {
                messages.Remove(message);
            }
            return tempmessages;
        }

        public static void Update()
        {
            tempmessages.Clear();
        }
    }
}