using Bank.Domain.Commons;

namespace Bank.Service.DTOs;

public class UserForResultDto : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
