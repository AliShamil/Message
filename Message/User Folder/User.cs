using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.User_Folder;

internal class User
{
    public readonly Guid ID;


    private string? username;
    private string? email;
    private string? password;
    private int age;

    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public string? Username
    {
        get { return username; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{nameof(value)} must be written !");

            if (value.Length<3)
                throw new ArgumentOutOfRangeException($"Username must be greater than 3 character!");

            username = value;
        }
    }

    public string? Email
    {
        get { return email; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{nameof(value)} must be written !");
            if (!IsValidEmail(value))
                throw new ArgumentException("Email is invalid !");

            email = value;
        }
    }

    public string? Password
    {
        get { return password; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException($"{nameof(value)} must be written !");

            if (value.Length<=3 ||value.Contains(username))
                throw new ArgumentException("Your password is weak!");
            password = value;
        }
    }

   

    public int Age
    {
        get { return age; }
        set 
        {
            if (value <15 || value >150)
                throw new ArgumentNullException("Your age must be (15 - 150) avarege !");
            age = value; 
        }
    }


    public User(string? _username, string? _email, string? _password,int _age)
    {
        Username=_username;
        Email=_email;
        Password=_password;
        Age=_age;
        ID = Guid.NewGuid();

    }




    public override string ToString()
    {
        return $@"ID: {ID}

    Username: {Username}
    Email: {Email}
    Password: {Password}
    Age: {Age}";
    }


}
