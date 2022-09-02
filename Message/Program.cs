using System.Text.Json;
namespace Message;
using Admin_Folder;
using User_Folder;
using Post_Folder;
using Notification_Folder;




class Prrogram
{
    #region Helper_Func
    public static Admin? Find(List<Admin>? admins, string? username, string? password)
    {
        for (int i = 0; i < admins!.Count; i++)
        {
            if (admins[i].Username==username&& admins[i].Password==password)
                return admins[i];
        }


        return null;
    }



    public static User? Find(List<User>? users, string? username, string? password)
    {
        for (int i = 0; i < users!.Count; i++)
        {
            if (users[i].Username == username && users[i].Password == password)
                return users[i];
        }

        return null;
    }

    public static Admin? Find(List<Admin>? admins, string? username)
    {
        foreach (var a in admins)
        {
            if (a.Username== username)
                return a;
        }
        return null;
    }
    public static bool Check(Post LikedUsers, string? username)
    {
        foreach (var user in LikedUsers.likedUsers)
        {
            return user == username;

        }
        return false;
    }

    public static bool SendNotification(Admin admin,string?username,string?text)
    {
        Notification not = new(text, username);
        admin.Notifications.Add(not);
        return false;
    }



    #endregion
    static void Main()
    {
        #region Load_Previous_Info
        List<Admin> admins;
        List<User> users;
        List<Post> posts;


        try
        {

            var adminJs = File.ReadAllText("adminJs.json");
            admins = JsonSerializer.Deserialize<List<Admin>>(adminJs);


            var userJs = File.ReadAllText("userJs.json");
            users = JsonSerializer.Deserialize<List<User>>(userJs);

            var postJs = File.ReadAllText("postJs.json");
            posts= JsonSerializer.Deserialize<List<Post>>(postJs);


            foreach (var a in admins)
            {

                for (int i = 0; i < posts.Count; i++)
                {
                    if (posts[i].Writer == a.Username)
                        a.Posts.Add(posts[i]);
                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Previous file not found!");
            Console.ReadKey(false);

            admins = new List<Admin>();
            users = new List<User>();
            posts = new List<Post>();
        }

        #endregion

        Admin Ali = new("eliwka", "elisamilzade@gmail.com", "Ali123");
        Ali.Posts.Add(new Post("Bomba post",Ali.Username));
        Ali.Posts.Add(new Post("Ela post", Ali.Username));
        Ali.Posts.Add(new Post("Fantastik post", Ali.Username));
        Ali.Posts.Add(new Post("Bomba post", Ali.Username));

        admins.Add(Ali);

        User Vasif = new("vasya", "vasya123@gmail.com", "Vasifcik", 18);
        users.Add(Vasif);


        Console.Clear();
        Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tWELCOME");
        Thread.Sleep(2000);
        bool messageProgram = false;

        while (!messageProgram)
        {
            Console.Clear();
            Console.Write(@"
1. Login
0. Exit

Pls select: ");
            switch (Console.ReadLine())
            {
                case "1":
                    string? username;
                    string? password;

                    bool login = false;
                    while (!login)
                    {
                        Console.Clear();
                        Console.Write(@"
1. As Admin
2. As User
0. Back

Pls select: ");
                                Post findPost;
                        switch (Console.ReadLine())
                        {
                            case "1":
                                Admin? admin = null;
                                bool adminLogin = false;
                                while (!adminLogin)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your username: ");
                                    username = Console.ReadLine();
                                    Console.Write("Enter you password: ");
                                    password = Console.ReadLine();



                                    admin = Find(admins, username, password);

                                    try
                                    {
                                        if (admin == null)
                                            throw new NullReferenceException("Admin not found!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(ex.Message);
                                        Thread.Sleep(1500);

                                        continue;
                                    }

                                    bool adminMenu = false;
                                    while (!adminMenu)
                                    {
                                        string? WriterUsername;
                                        string? PostId;
                                        Console.Clear();
                                        Console.Write(@"
1. Show Own Posts
2. Add Posts
3. Like Posts
4. Show Notifications
0. Back

Pls select: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                Console.Clear();
                                                admin.ShowPost();
                                                Console.ReadKey(false);
                                                break;
                                            case "2":
                                                Console.Clear();
                                                Console.WriteLine("Enter content:");
                                                try
                                                {
                                                    admin.AddPost(Console.ReadLine(), admin.Username);

                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);
                                                    Thread.Sleep(1500);
                                                    continue;
                                                }
                                                Console.Clear();
                                                break;
                                            case "3":

                                                bool likeAdmin = false;
                                                while (!likeAdmin)
                                                {
                                                Console.Clear();
                                                Console.WriteLine("Enter Post ID: ");
                                                PostId = Console.ReadLine();
                                                Console.WriteLine("Enter Writer username: ");
                                                WriterUsername = Console.ReadLine();
                                                    try
                                                    {
                                                        if (string.IsNullOrWhiteSpace(WriterUsername) || string.IsNullOrWhiteSpace(PostId))
                                                            throw new ArgumentException("Username or Post ID is invalid!");

                                                        findPost = Find(admins, WriterUsername)?.FindPost(PostId);
                                                        if (findPost == null)
                                                            throw new NullReferenceException("This post not found !");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                        continue;
                                                    }
                                                    while (!likeAdmin)
                                                    {
                                                        ++findPost.ViewCount;
                                                        SendNotification(Find(admins, WriterUsername), admin.Username, $"{username} viewed your post!");
                                                        Console.Clear();
                                                        Console.WriteLine(findPost);
                                                        Console.Write(@"
1. Like
0. Back

Pls select: ");
                                                        switch (Console.ReadLine())
                                                        {
                                                            case "1":
                                                                if(!Check(findPost, admin.Username))
                                                                {
                                                                    ++findPost.LikeCount;
                                                                    SendNotification(Find(admins, WriterUsername), admin.Username, $"{username} liked your post!");
                                                                }
                                                                likeAdmin = true;
                                                                break;

                                                            case "0":
                                                                likeAdmin = true;
                                                                break;
                                                        
                                                            default:
                                                                try
                                                                {
                                                                    throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    Console.Clear();
                                                                    Console.WriteLine(ex.Message);

                                                                    Thread.Sleep(1500);
                                                                    continue;
                                                                }
                                                                
                                                        }
                                                    }
                                                }

                                                break;
                                            case "4":
                                                Console.Clear();
                                                admin.ShowNotifications();
                                                Console.ReadKey(false);
                                                break;
                                            case "0":
                                                adminMenu = true;
                                                adminLogin = true;
                                                break;
                                            default:
                                                try
                                                {
                                                    throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);

                                                    Thread.Sleep(1500);
                                                    continue;
                                                }
                                                
                                        }
                                    }
                                }
                                break;

                            case "2":
                                User? user = null;
                                bool userLogin = false;
                                while (!userLogin)
                                {
                                    Console.Clear();
                                    Console.Write("Enter your username: ");
                                    username = Console.ReadLine();
                                    Console.Write("Enter you password: ");
                                    password = Console.ReadLine();



                                    user = Find(users, username, password);

                                    try
                                    {
                                        if (user == null)
                                            throw new NullReferenceException("User not found!");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(ex.Message);
                                        Thread.Sleep(1500);

                                        continue;
                                    }

                                    bool userMenu = false;
                                    while (!userMenu)
                                    {
                                        string? WriterUsername;
                                        string? PostId;
                                        Console.Clear();
                                        Console.Write(@"
1. Show Posts
2. Like Posts
0. Back

Pls select: ");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                Console.Clear();
                                                if(posts.Count==0)
                                                    Console.WriteLine("EMPTY");
                                                foreach (var p in posts)
                                                {
                                                    Console.WriteLine(p);
                                                }
                                                Console.ReadKey(false);
                                                break;
                                           
                                            case "2":

                                                bool likeUser = false;
                                                while (!likeUser)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Post ID: ");
                                                    PostId = Console.ReadLine();
                                                    Console.WriteLine("Enter Writer username: ");
                                                    WriterUsername = Console.ReadLine();
                                                    try
                                                    {
                                                        if (string.IsNullOrWhiteSpace(WriterUsername) || string.IsNullOrWhiteSpace(PostId))
                                                            throw new ArgumentException("Username or Post ID is invalid!");

                                                        findPost = Find(admins, WriterUsername)?.FindPost(PostId);
                                                        if (findPost == null)
                                                            throw new NullReferenceException("This post not found !");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine(ex.Message);
                                                        Thread.Sleep(1500);

                                                        continue;
                                                    }
                                                    while (!likeUser)
                                                    {
                                                        ++findPost.ViewCount;
                                                        SendNotification(Find(admins, WriterUsername), user.Username, $"{username} viewed your post!");
                                                        Console.Clear();
                                                        Console.WriteLine(findPost);
                                                        Console.Write(@"
1. Like
0. Back

Pls select: ");
                                                        switch (Console.ReadLine())
                                                        {
                                                            case "1":
                                                                if (!Check(findPost, user.Username))
                                                                {
                                                                    ++findPost.LikeCount;
                                                                    SendNotification(Find(admins, WriterUsername), user.Username, $"{username} liked your post!");
                                                                }
                                                                likeUser = true;
                                                                break;

                                                            case "0":
                                                                likeUser = true;
                                                                break;

                                                            default:
                                                                try
                                                                {
                                                                    throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                                                                }
                                                                catch (Exception ex)
                                                                {

                                                                    Console.Clear();
                                                                    Console.WriteLine(ex.Message);

                                                                    Thread.Sleep(1500);
                                                                    continue;
                                                                }

                                                        }
                                                    }
                                                }

                                                break;
                                          case "0":
                                                userMenu = true;
                                                userLogin = true;
                                                break;
                                            default:
                                                try
                                                {
                                                    throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                                                }
                                                catch (Exception ex)
                                                {

                                                    Console.Clear();
                                                    Console.WriteLine(ex.Message);

                                                    Thread.Sleep(1500);
                                                    continue;
                                                }

                                        }
                                    }
                                }

                                break;
                            case "0":
                                login = true;
                                break;

                            default:
                                try
                                {
                                    throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                                }
                                catch (Exception ex)
                                {

                                    Console.Clear();
                                    Console.WriteLine(ex.Message);

                                    Thread.Sleep(1500);
                                    continue;
                                }
                        }
                    }
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\t\t\t\t\t\tSEE YOU LATER !");
                    messageProgram = true;
                    break;
                default:
                    try
                    {
                        throw new ArgumentException("\n\n\n\n\n\n\n\n\t\t\t\t\t\tUnknown command!");
                    }
                    catch (Exception ex)
                    {

                        Console.Clear();
                        Console.WriteLine(ex.Message);

                        Thread.Sleep(1500);
                        continue;
                    }


            }
        }

        foreach (var Admin in admins)
        {
            foreach (var Post in Admin.Posts)
            {
                posts?.Add(Post);
            }
        }

        var adminsWrite = JsonSerializer.Serialize(admins);
        File.WriteAllText("adminJs.json", adminsWrite);

        var usersWrite = JsonSerializer.Serialize(users);
        File.WriteAllText("userJs.json", usersWrite);

        var postsWrite = JsonSerializer.Serialize(posts);
        File.WriteAllText("postJs.json", postsWrite);
    }
}