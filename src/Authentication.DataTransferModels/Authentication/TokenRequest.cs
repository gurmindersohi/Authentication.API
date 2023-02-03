namespace Authentication.DataTransferModels.Authentication
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class TokenRequest
    {
        /// <summary>
        /// The username of the user logging in.
        /// </summary>
        [Required]
        [StringLength(20)]
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// The password for the user logging in.
        /// </summary>
        [Required]
        [StringLength(30)]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}

