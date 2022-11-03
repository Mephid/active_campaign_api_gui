using ac_api_gui.Models;
using Contact.Models;
using Contact.Service;
using EventAggregator.Core.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Diagnostics;
using Utility.DTOs;

namespace Contact.ViewModels
{
    public class ContactViewModel : BindableBase
    {

        private IEventAggregator _ea;
        private IContactBulkOptionsService _cbos;
        public DelegateCommand GetFields { get; private set; }
        public DelegateCommand CreateOptions { get; private set; }

        private List<Field> _fields;
        public List<Field> Fields { get { return _fields; } set { SetProperty(ref _fields, value); } }

        private string _apiKey;
        public string ApiKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }

        private string _accountName;
        private string AccountName { get { return _accountName; } set { SetProperty(ref _accountName, value); } }

        private string _selectedFieldId;
        public string SelectedFieldId { get { return _selectedFieldId; } set { SetProperty(ref _selectedFieldId, value); } }

        private string _optionsText;
        public string OptionsText { get { return _optionsText; } set { SetProperty(ref _optionsText, value); } }

        private bool _isLoading = false;
        public bool IsLoading { get { return _isLoading; } set { SetProperty(ref _isLoading, value); } }

        public ContactViewModel(IEventAggregator ea, IContactBulkOptionsService cbos)
        {
            _ea = ea;
            _cbos = cbos;

            ea.GetEvent<ApiKeyChanged>().Subscribe(ak => { ApiKey = ak; });
            ea.GetEvent<AccountNameChanged>().Subscribe(an => { AccountName = an; });

            GetFields = new DelegateCommand(GetFieldsExecute, GetFieldsCanExecute)
                .ObservesProperty(() => ApiKey)
                .ObservesProperty(() => AccountName)
                .ObservesProperty(() => IsLoading);

            CreateOptions = new DelegateCommand(CreateOptionsExecute, CreateOtionsCanExecute)
                .ObservesProperty(() => ApiKey)
                .ObservesProperty(() => AccountName)
                .ObservesProperty(() => SelectedFieldId)
                .ObservesProperty(() => IsLoading);
        }

        private async void GetFieldsExecute()
        {
            IsLoading = true;
            ContactbulkOptionsServiceResponseDTO<List<Field>> serviceResponse = await _cbos.GetFields(AccountName, ApiKey);

            if (serviceResponse.Payload is not null)
            {
                Fields = serviceResponse.Payload;
            }

            _ea.GetEvent<ResponseChanged>().Publish(
                new ResponsePayload
                {
                    Message = serviceResponse.Message,
                    Status = serviceResponse.Status
                }
            );
            IsLoading = false;
        }
        private bool GetFieldsCanExecute()
        {
            // TODO: Validation (crash if accName is invealid for url)
            // TODO: Map error message to human understandable
            // TODO: Add loading spinner if request is ongoing
            return !string.IsNullOrWhiteSpace(AccountName) && !string.IsNullOrWhiteSpace(ApiKey) && !IsLoading;
        }

        private async void CreateOptionsExecute()
        {
            IsLoading = true;
            ContactbulkOptionsServiceResponseDTO serviceResponse = await _cbos.CreateOptions(AccountName, ApiKey, SelectedFieldId, OptionsText);

            _ea.GetEvent<ResponseChanged>().Publish(
                new ResponsePayload
                {
                    Message = serviceResponse.Message,
                    Status = serviceResponse.Status
                }
            );
            IsLoading = false;
        }

        private bool CreateOtionsCanExecute()
        {
            return !string.IsNullOrWhiteSpace(AccountName) && !string.IsNullOrWhiteSpace(ApiKey) && !string.IsNullOrWhiteSpace(SelectedFieldId) && !IsLoading;
        }


    }
}
