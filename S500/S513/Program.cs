using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
Environment.SetEnvironmentVariable("TEST_AGE", "18");
Environment.SetEnvironmentVariable("TEST_CONTACTINFO:EMAILADDRESS", "foobar@outlook.com");
Environment.SetEnvironmentVariable("TEST_CONTACTINFO__PHONENO", "123456789");

var profile = new ConfigurationBuilder()
    .AddEnvironmentVariables("TEST_")//设置环境变量前缀后，配置项的名称会将此前缀剔除
    .Build()
    .Get<Profile>();//所以这里可以直接绑定到名为GENDER的属性，而不是TEST_GENDER

Console.WriteLine(profile.Gender == Gender.Male);
Console.WriteLine(profile.Age == 18);
Console.WriteLine(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
Console.WriteLine(profile.ContactInfo.PhoneNo == "123456789");
