//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace MotoresJogosFase1
{
    public enum MessageType
    {
        Console,
        Particles,
        Sound
    }
    public class Message
    {
        private MessageType messageType;
        public MessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private bool read;

        public bool Read
        {
            get { return read; }
            set { read = value; }
        }

        public Message(MessageType messageType)
        {
            this.messageType = messageType;
            this.read = false;
        }
    }
}
