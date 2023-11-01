using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieApp.Models
{
    public class SignUpModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [BsonElement("username")]
        public string? Username { get; set; }

         [BsonElement("role")]
        public string Role { get; set; } = "User";

        [Required]
        [BsonElement("emailaddress")]
        [EmailAddress]
        [Remote("IsUnique", "Validation", ErrorMessage = "User Already Exists.")]
        public string? EmailAddress { get; set; }

        [Required]
        [BsonElement("password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "The password must contain at least one lowercase letter, one uppercase letter, and one digit.")]
        public string? Password { get; set; }

        [Required]
        [BsonElement("confirmpassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}