using System;
using System.Messaging;

namespace MsmqTester
{
    public static class MessageQueueBuilder
    {
        public static MessageQueue IncludeArrivedTime(this MessageQueue queue)
        {
            queue.MessageReadPropertyFilter.ArrivedTime = true;
            return queue;
        }

        public static MessageQueue IncludeCorrelationId(this MessageQueue queue)
        {
            queue.MessageReadPropertyFilter.CorrelationId = true;
            return queue;
        }

        public static MessageQueue IncludeLookupId(this MessageQueue queue)
        {
            queue.MessageReadPropertyFilter.LookupId = true;
            return queue;
        }

        public static MessageQueue WithStringFormatter(this MessageQueue queue)
        {
            // Set the formatter to indicate body contains a string.
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            return queue;
        }
    }
}