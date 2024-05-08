
namespace Domain.Entities;

public class User

{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }


    public List<Stock>? Stocks { get; set; }


}
