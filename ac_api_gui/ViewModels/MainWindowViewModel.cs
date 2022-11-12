using EventAggregator.Core.Events;
using Microsoft.VisualBasic;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ac_api_gui.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDataErrorInfo
    {

        // TODO: Emit an error event if this[string name] -> result isn't null.
        // Replace the check in SetProperty with a check on the existence of the error in interested components.

        private string _accountName = "";
        public string AccountName
        {
            get { return _accountName; }
            set
            {
                SetProperty(ref _accountName, value);

                if (this["AccountName"] == null) {
                    _eventAggregator.GetEvent<AccountNameChanged>().Publish(value);
                } else
                {
                    _eventAggregator.GetEvent<AccountNameChanged>().Publish("");
                }
            }
        }

        private string _apiKey = "";
        public string ApiKey
        {
            get { return _apiKey; }
            set
            {
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

        public string Error => null;

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "AccountName":
                        result = ValidateAccountName();
                        break;

                    default:
                        break;
                }

                return result;
            }
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

        private string ValidateAccountName()
        {
            string result = null;

            if (!Uri.IsWellFormedUriString(AccountName, UriKind.RelativeOrAbsolute))
            {
                result = "Please insert a valid Account Name";
            }

            return result;
        }

    }
}
