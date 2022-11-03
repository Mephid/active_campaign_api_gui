using System.Text.Json.Nodes;
using System.Text.Json;
using Utility.DTOs;
using System.Net;

namespace Utility
{
    public class ApiCallUtils
    {
        private static async Task<T> DeserializeResponse<T>(HttpResponseMessage response, string propertyName)
        {
            JsonNode responseContent = JsonNode.Parse(await response.Content.ReadAsStringAsync());
            return JsonSerializer.Deserialize<T>(responseContent![propertyName].ToJsonString(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static async Task<ContactbulkOptionsServiceResponseDTO<T>> CreateResponseDTO<T>(HttpResponseMessage response, string propertyName)
        {
            ContactbulkOptionsServiceResponseDTO<T> responseDTO = new();

            if (response.IsSuccessStatusCode)
            {
                responseDTO.Status = "OK";
                responseDTO.Message = response.StatusCode.ToString();
                responseDTO.Payload = await DeserializeResponse<T>(response, propertyName);
            }
            else
            {
                responseDTO.Status = "KO";
                responseDTO.Message = response.StatusCode.ToString();
            }

            return responseDTO;
        }

        public static ContactbulkOptionsServiceResponseDTO CreateResponseDTO(HttpResponseMessage response)
        {
            ContactbulkOptionsServiceResponseDTO responseDTO = new();

            if (response.IsSuccessStatusCode)
            {
                responseDTO.Status = "OK";
                responseDTO.Message = response.StatusCode.ToString();
            }
            else
            {
                responseDTO.Status = "KO";
                responseDTO.Message = response.StatusCode.ToString();
            }

            return responseDTO;
        }

        public static ContactbulkOptionsServiceResponseDTO<T> CreateServerErrorDTO<T>()
        {
            ContactbulkOptionsServiceResponseDTO<T> responseDTO = new();

            responseDTO.Status = "KO";
            responseDTO.Message = HttpStatusCode.InternalServerError.ToString();

            return responseDTO;
        }
                
        public static ContactbulkOptionsServiceResponseDTO CreateServerErrorDTO()
        {
            ContactbulkOptionsServiceResponseDTO responseDTO = new();

            responseDTO.Status = "KO";
            responseDTO.Message = HttpStatusCode.InternalServerError.ToString();

            return responseDTO;
        }

    }
}