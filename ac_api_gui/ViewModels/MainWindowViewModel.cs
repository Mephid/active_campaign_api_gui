using ac_api_gui.Models;
using EventAggregator.Core.Events;
using Prism.Events;
using Prism.Mvvm;

namespace ac_api_gui.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _accountName = null;
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                SetProperty(ref _accountName, value);
                _eventAggregator.GetEvent<AccountNameChanged>().Publish(value);
            }
        }

        private string _apiKey = null;
        public string ApiKey
        {
            get { return _apiKey; }
            set { 
                SetProperty(ref _apiKey, value);
                _eventAggregator.GetEvent<ApiKeyChanged>().Publish(value); 
            }
        }

        private string _responseStatus;
        public string ResponseStatus
        {
            get { return _responseStatus; }
            set { SetProperty(ref _responseStatus, value); }

        }

        private string _responseMessage;
        public string ResponseMessage
        {
            get { return _responseMessage; }
            set { SetProperty(ref _responseMessage, value); }
        }

        IEventAggregator _eventAggregator;
        public MainWindowViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;

            ea.GetEvent<ResponseChanged>().Subscribe(payload =>
            {
                ResponseStatus = payload.Status;
                ResponseMessage = payload.Message;
            });
        }

    }
}
