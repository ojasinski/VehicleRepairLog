﻿using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLogUI.Services.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient,
                           IConfiguration configuration,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            httpClient.BaseAddress = new Uri(configuration["ApiUri"]);
        }

        /// <summary>
        /// Calls API in order to get data for User of given ID
        /// </summary>
        /// <param name="id">ID of the <see cref="User"/> the data will be returned for</param>
        /// <returns>Object of type <see cref="User"/> with user data</returns>
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Calls the API.
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/Users/{id}");

            // If status code returned by API not successful...
            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            // Otherwise map data returned by API into User object.
            var user = await response.Content.ReadFromJsonAsync<UserDto>();

            return user;
        }

        /// <summary>
        /// Calls API in order to update data for current <see cref="User"/>
        /// </summary>
        /// <param name="user">Placeholder for all updated <see cref="User"/> data</param>
        /// <returns>Object of type <see cref="User"/> with updated data</returns>
        public async Task<UserDto> UpdateUserDetailsAsync(UserDto user)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Calls the API.
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Users/{user.Id}", user);

            // If status code returned by API not successful...
            if (response.IsSuccessStatusCode == false)
            {
                return null;
            }

            // Otherwise map data returned by API into User object.
            user = await response.Content.ReadFromJsonAsync<UserDto>();

            // Return updated User details.
            return user;
        }

        /// <summary>
        /// Calls API in order to change password for current <see cref="User"/>
        /// </summary>
        /// <param name="passwordChangeRequest">Placeholder for new password</param>
        /// <param name="userId">Placeholder for <see cref="User"/> ID</param>
        public async Task ChangePasswordAsync(PasswordChangeRequestDto passwordChangeRequest, int userId)
        {
            // Gets authentication Token from local storage.
            string token = await _localStorage.GetItemAsync<string>("authToken");

            // Adds authentication Token to the authentication header.
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Calls the API.
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Users/PasswordChange/{userId}", passwordChangeRequest);

            // If status code returned by API not successful...
            if (response.IsSuccessStatusCode == false)
            {
                // NAPISAĆ LOGIKĘ DO OBSŁUGI BŁĘDÓW
                throw new Exception();
            }
        }
    }
}
