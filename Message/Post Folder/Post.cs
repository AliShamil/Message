using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Post_Folder;

internal class Post
{

    public readonly Guid ID;
    public readonly DateTime CreationDateTime;
    public readonly List<string?> likedUsers;

    private string? content;
    private string? writer;
    private int likeCount;
    private int viewCount;


    public string? Content
    {
        get { return content; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Content is not written");

            content = value;

        }
    }


    public string? Writer
    {
        get { return writer; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Writer is not writen!");

            writer = value;
        }
    }


    public int LikeCount
    {

        get { return likeCount; }

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be greater than 0 !");

            likeCount = value;
        }
    }


    public int ViewCount
    {

        get { return viewCount; }

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be greater than 0 !");

            viewCount = value;
        }
    }



    public Post(string? _content, string? _writer)
    {
        Content=_content;
        Writer=_writer;
        LikeCount=0;
        ViewCount=0;
        ID = Guid.NewGuid();
        CreationDateTime=DateTime.Now;
        likedUsers = new List<string?>();
    }


    public override string ToString()
    {
        return $@"ID: {ID}

    Content: {Content}
    Like Count: {LikeCount}
    View Count: {ViewCount}
    Creation Date Time: {CreationDateTime}";


    }
}
