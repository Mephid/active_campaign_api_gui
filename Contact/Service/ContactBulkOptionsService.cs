using Contact.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Utility.DTOs;
using Utility;
using System.Text.Json;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Contact.Service
{

    public class ContactBulkOptionsService : IContactBulkOptionsService
    {
        private class FieldOption
        {
            public int field { get; set; }
            public string value { get; set; }
        }

        private class CreateOptionsDTO
        {
            public List<FieldOption> fieldOptions { get; private set; }

            public CreateOptionsDTO(int fieldId, string[] values)
            {
                fieldOptions = new List<FieldOption>();
                foreach (var value in values)
                {
                    fieldOptions.Add(new FieldOption
                    {
                        field = fieldId,
                        value = value
                    });
                }
            }
        }

        private readonly HttpClient client = new HttpClient();

        public async Task<ContactbulkOptionsServiceResponseDTO<List<Field>>> GetFields(string accountName, string apiKey)
        {
            client.DefaultRequestHeaders.Add("Api-Token", apiKey);
            ContactbulkOptionsServiceResponseDTO<List<Field>> responseDTO;

            try
            {
                HttpResponseMessage response = await client.GetAsync(new Uri($"https://{accountName}.api-us1.com/api/3/fields?limit=100"));
                responseDTO = await ApiCallUtils.CreateResponseDTO<List<Field>>(response, "fields");
            }
            catch (Exception)
            {
                responseDTO = ApiCallUtils.CreateServerErrorDTO<List<Field>>();
            }
            client.DefaultRequestHeaders.Remove("Api-Token");

            return responseDTO;
        }

        public async Task<ContactbulkOptionsServiceResponseDTO> CreateOptions(string accountName, string apiKey, string fieldId, string valuesFullText)
        {
            client.DefaultRequestHeaders.Add("Api-Token", apiKey);
            ContactbulkOptionsServiceResponseDTO responseDTO;

            string[] options = Regex
            .Split(valuesFullText, "\r?\n")
            .Where(option => !string.IsNullOrWhiteSpace(option))
            .Select(option => option.Trim())
            .ToArray();

            try
            {
                StringContent jsonContent = new(JsonSerializer.Serialize(new CreateOptionsDTO(int.Parse(fieldId), options)), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(new Uri($"https://{accountName}.api-us1.com/api/3/fieldOption/bulk"), jsonContent);
                responseDTO = ApiCallUtils.CreateResponseDTO(response);
            }
            catch (Exception)
            {
                responseDTO = ApiCallUtils.CreateServerErrorDTO();
            }

            client.DefaultRequestHeaders.Remove("Api-Token");
            return responseDTO;

        }
    }
}
