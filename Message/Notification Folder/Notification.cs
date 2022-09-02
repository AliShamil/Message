using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Notification_Folder;

internal class Notification
{
    public readonly Guid ID;
    public readonly DateTime SendTime;
    public string? text;
    public string? fromUser;

    public string? Text
    {
        get { return text; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Text must be written!");
            text = value;
        }
    }

    public string? FromUser
    {
        get { return fromUser; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("FromUser must be written!");
            fromUser = value;
        }
    }



    public Notification(string? text, string? fromUser)
    {
        ID = Guid.NewGuid();
        Text=text;
        SendTime=DateTime.Now;
        FromUser=fromUser;
    }

    public override string ToString()
    {
        return $@"Id: {ID}

Text: {Text}
Send Time: {SendTime}
From User: {FromUser}";
    }
}
