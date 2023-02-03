namespace Authentication.Infrastructure.Identity.Models.Authentication
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class TokenRequest
    {
        /// <summary>
        /// The username of the user logging in.
        /// </summary>
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// The password for the user logging in.
        /// </summary>
        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

