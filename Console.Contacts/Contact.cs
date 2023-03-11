namespace Console.Contacts;

public sealed class Contact
{
    public string Name { get; set; }

    public string Phone { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Phone}";
    }
}
