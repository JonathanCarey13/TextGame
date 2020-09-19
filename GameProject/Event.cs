using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Event
    {
        public enum EventType { Get, Use }
        public EventType Type;
        public string TriggerPhrase;
        public Result EventResult;

        public Event(string triggerPhrase, EventType type, Result eventResult)
        {
            TriggerPhrase = triggerPhrase;
            Type = type;
            EventResult = eventResult;
        }
    }

    public class Result
    {
        public enum ResultType { GetItem, MessageOnly, }
        public ResultType Type { get; }

        public string ResultMessage { get; }

        public ProgramUI.Item ResultItem { get; }

        public Result(ProgramUI.Item resultItem, string resultMessage)
        {
            Type = ResultType.GetItem;
            ResultItem = resultItem;
            ResultMessage = resultMessage;
        }


        public Result(string resultMessage)
        {
            Type = ResultType.MessageOnly;
            ResultMessage = resultMessage;
        }

    }
}
