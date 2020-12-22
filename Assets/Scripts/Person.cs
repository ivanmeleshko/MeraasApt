

public class Person
{

    public static Person Instance;

    public string Fname
    { get; set; }

    public string Sname
    { get; set; }

    public string Email
    { get; set; }

    public string Phone
    { get; set; }

    public string Description
    { get; set; }


    public Person() { }


    public Person(string _fname, string _sname, string _email, string _phone, string _description)
    {
        Fname = _fname;
        Sname = _sname;
        Email = _email;
        Phone = _phone;
        Description = _description;
    }
}
