using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["foo:gender"] = "Male",
    ["foo:age"] = "18",
    ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
    ["foo:contactInfo:phoneNo"] = "123",

    ["bar:gender"] = "Male",
    ["bar:age"] = "25",
    ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
    ["bar:contactInfo:phoneNo"] = "456",

    ["baz:gender"] = "Female",
    ["baz:age"] = "36",
    ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
    ["baz:contactInfo:phoneNo"] = "789"
};

//以IDictionary<string,Profile>形式来绑定
var profiles = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build()
    .Get<IDictionary<string,Profile>>();;

Console.WriteLine(profiles["foo"].ContactInfo.EmailAddress == "foo@outlook.com");
Console.WriteLine(profiles["bar"].ContactInfo.EmailAddress == "bar@outlook.com");
Console.WriteLine(profiles["baz"].ContactInfo.EmailAddress == "baz@outlook.com");