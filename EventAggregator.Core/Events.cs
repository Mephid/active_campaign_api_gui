using ac_api_gui.Models;
using Prism.Events;

namespace EventAggregator.Core.Events
{
    public class AccountNameChanged : PubSubEvent<string> { }
    public class ApiKeyChanged : PubSubEvent<string> { }
    public class ResponseChanged : PubSubEvent<ResponsePayload> { }

}
