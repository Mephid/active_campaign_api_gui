using Prism.Mvvm;

namespace ac_api_gui.Models
{
    public class ResponsePayload: BindableBase
    {
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
