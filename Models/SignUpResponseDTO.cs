namespace Models;

public class SignUpResponseDTO
{
    public SignUpResponseDTO()
    {
        Errors = new List<string>();
    }

    public bool IsRegisterationSuccessful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
