namespace App
{
    public class ServiceRegistry
    {
        //服务类型
        public Type ServiceType { get; }
        //生命周期
        public Lifetime Lifetime { get; }
        //创建服务实例的工厂方法
        public Func<Cat, Type[], object?> Factory { get; }
        //组成一个单向链表，包含所有同一服务类型的服务注册
        internal ServiceRegistry? Next { get; set; }

        //Type[]为服务类型的泛型参数集合
        public ServiceRegistry(Type serviceType, Lifetime lifetime, Func<Cat, Type[], object?> factory)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
            Factory = factory;
        }

        //取出链表中的所有服务注册组成一个集合
        internal IEnumerable<ServiceRegistry> AsEnumerable()
        {
            var list = new List<ServiceRegistry>();
            for (var self = this; self != null; self = self.Next)
            {
                list.Add(self);
            }
            return list;
        }
    }
}
