using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatStuff.Core.DTOs;

public record class UserDTO
(
    [Required]
    string UserName,

    [Required]
    [PasswordPropertyText]
    string Password
);
