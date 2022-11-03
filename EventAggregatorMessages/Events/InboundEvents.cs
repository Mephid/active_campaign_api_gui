using ac_api_gui.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ac_api_gui.Events
{
    public class ResponseStatusChanged : PubSubEvent<ResponseStatus> { }
}
