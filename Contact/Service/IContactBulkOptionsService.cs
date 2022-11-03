using Contact.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.DTOs;

namespace Contact.Service
{
    public interface IContactBulkOptionsService
    {        
        Task<ContactbulkOptionsServiceResponseDTO> CreateOptions(string accountName, string apiKey, string fieldId, string valuesFullText);
        Task<ContactbulkOptionsServiceResponseDTO<List<Field>>> GetFields(string accountName, string apiKey);
    }
}