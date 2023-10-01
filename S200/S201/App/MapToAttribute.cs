
namespace App
{
    //程序集范围的批量服务注册时标识该服务需要被注册
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MapToAttribute : Attribute
    {
        public Type ServiceType { get; }
        public Lifetime Lifetime { get; }
        public MapToAttribute(Type serviceType, Lifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
    }
}