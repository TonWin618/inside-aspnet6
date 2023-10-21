using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["0:gender"] = "Male",
    ["0:age"] = "18",
    ["0:contactInfo:emailAddress"] = "foo@outlook.com",
    ["0:contactInfo:phoneNo"] = "123",

    ["1:gender"] = "Male",
    ["1:age"] = "25",
    ["1:contactInfo:emailAddress"] = "bar@outlook.com",
    ["1:contactInfo:phoneNo"] = "456",

    ["2:gender"] = "Female",
    ["2:age"] = "36",
    ["2:contactInfo:emailAddress"] = "baz@outlook.com",
    ["2:contactInfo:phoneNo"] = "789"
};

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

//绑定到一个Profile列表
var list = configuration.Get<IList<Profile>>();
Console.WriteLine(list[0].ContactInfo.EmailAddress == "foo@outlook.com");
Console.WriteLine(list[1].ContactInfo.EmailAddress == "bar@outlook.com");
Console.WriteLine(list[2].ContactInfo.EmailAddress == "baz@outlook.com");

//绑定到一个Profile数组
var array = configuration.Get<Profile[]>();
Console.WriteLine(array[0].ContactInfo.EmailAddress == "foo@outlook.com");
Console.WriteLine(array[1].ContactInfo.EmailAddress == "bar@outlook.com");
Console.WriteLine(array[2].ContactInfo.EmailAddress == "baz@outlook.com");
