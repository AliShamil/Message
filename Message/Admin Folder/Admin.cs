using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Admin_Folder;
using Post_Folder;
using Notification_Folder;
internal class Admin
{
    public readonly Guid ID;
    public readonly List<Post> Posts;
    public readonly List<Notification> Notifications;

    private string? username;
    private string? email;
    private string? password;


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
            if(string.IsNullOrWhiteSpace(value))
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

            if (value.Length<=3 ||value.Contains(username!))
                throw new ArgumentException("Your password is weak!");
            password = value; 
        }
    }

    public Admin(string? _username, string? _email, string? _password)
    {
        Username=_username;
        Email=_email;
        Password=_password;
        ID = Guid.NewGuid();
        Posts = new List<Post>();   
        Notifications = new List<Notification>();
    }

    public void AddPost(string content, string writer)
    {
        Posts.Add(new Post(content, writer));
    }

    public void ShowPost()
    {
        foreach (var post in Posts)
        {
            Console.WriteLine(post);
            
            Console.WriteLine();
        }
    }


    public void ShowNotifications()
    {
        if (Notifications.Count == 0)
            Console.WriteLine("EMPTY");

        foreach (var notification in Notifications)
        {
            Console.WriteLine(notification);
            
            Console.WriteLine();
        }
    }

    public Post? FindPost(string id)
    {
        foreach (var post in Posts)
        {
            if (post.ID==Guid.Parse(id))
                return post;
        }

        return null;
    }


    public override string ToString()
    {
        return $@"ID: {ID}

Username: {Username}
Email: {Email}
Password: {Password}";
    }


}
