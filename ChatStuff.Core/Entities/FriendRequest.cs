using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatStuff.Core.Entities;
public class FriendRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string SourceUserName { get; set; } = default!; 
    [Required]
    public string TargetUserName { get; set; } = default!; 
}