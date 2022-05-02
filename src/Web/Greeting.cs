using System.ComponentModel.DataAnnotations;

public class Greeting
{
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    public string Text { get; set; }
}
