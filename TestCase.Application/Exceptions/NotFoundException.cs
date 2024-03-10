namespace TestCase.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? name = null) 
        : base(name == null ? "Item was not found" : $"{name} was not found")
    {
            
    }
}