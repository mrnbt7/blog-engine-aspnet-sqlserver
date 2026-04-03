using System;

namespace NbtBlogEngine.Models
{
    /// <summary>
    /// Data transfer object representing a user.
    /// </summary>
    public class UserDTO
    {
        /// <summary>Gets or sets the unique identifier of the user.</summary>
        public long Id { get; set; }

        /// <summary>Gets or sets the user's first name.</summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the user's last name.</summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the user's email address.</summary>
        public string Email { get; set; }

        /// <summary>Gets or sets the date the user registered.</summary>
        public DateTime? RegisteredAt { get; set; }

        /// <summary>Gets or sets the date of the user's last login.</summary>
        public DateTime? LastLogin { get; set; }

        /// <summary>Gets or sets the total number of posts by this user.</summary>
        public int PostCount { get; set; }
    }
}
