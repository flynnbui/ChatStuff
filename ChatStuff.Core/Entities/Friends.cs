using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatStuff.Core.Entities;

public class Friends
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string FriendName1 { get; set; } = default!; 
    [Required]
    public string FriendName2 { get; set; } = default!; 

}