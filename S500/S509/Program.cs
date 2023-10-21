using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["gender"] = "Male",
    ["age"] = "18",
    ["contactInfo:emailAddress"] = "foobar@outlook.com",
    ["contactInfo:phoneNo"] = "123456789"
};

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

var profile = configuration.Get<Profile>();
Console.WriteLine(profile.Gender == Gender.Male);
Console.WriteLine(profile.Age == 18);
Console.WriteLine(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
Console.WriteLine(profile.ContactInfo.PhoneNo == "123456789");