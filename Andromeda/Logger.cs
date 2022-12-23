using System;
using System.Collections.Concurrent;

namespace Andromeda
{
    public static class Logger
    {
        private static ConcurrentQueue<LogMessage> _messageQueue = new ConcurrentQueue<LogMessage>();
        private static event EventHandler MessageQueued;

        private static bool _clearing = false;
        
        static Logger()
        {
            MessageQueued += ClearQueue;
        }

        public static string IndentWith(string with, string on)
        {
            string ret = "" + with;
            foreach (char c in on)
            {
                if (c == '\n')
                {
                    ret += '\n' + with;
                }
                else
                {
                    ret += c;
                }
            }

            return ret;
        }
        
        private static void ClearQueue(object sender, EventArgs e)
        {
            if (_clearing)
            {
                return;   
            }
            _clearing = true;
            while (!_messageQueue.IsEmpty)
            {
                _messageQueue.TryDequeue(out LogMessage msg);
                Console.WriteLine($"[{msg.Level}]: {msg.Message}");
            }

            _clearing = false;
        }

        public static void Log(LogMessage m)
        {
            _messageQueue.Enqueue(m);
            OnMessageQueued();
        }
        
        public static void Log(string message, LogLevel logLevel)
        {
            Log(new LogMessage(message, logLevel));
        }
        public static void Log(string message)
        {
            Log(new LogMessage(message, LogLevel.Debug));
        }

        private static void OnMessageQueued()
        {
            MessageQueued?.Invoke(null, EventArgs.Empty);
        }
    }

    public enum LogLevel
    {
        None,
        Error,
        Debug
    }
    
    public struct LogMessage
    {
        public string Message;
        public LogLevel Level;

        public LogMessage(string message, LogLevel level)
        {
            Message = message;
            Level = level;
        }
    }
}