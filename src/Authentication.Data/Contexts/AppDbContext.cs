namespace Authentication.Data.Contexts
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class AppDbContext
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEnabled { get; set; }

        [IgnoreDataMember]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //[JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}

