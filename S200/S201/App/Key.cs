namespace App
{
    internal class Key : IEquatable<Key>
    {
        //服务注册链表
        public ServiceRegistry Registry { get; }
        //依赖服务类型数组
        public Type[] GenericArguments { get; }

        public Key(ServiceRegistry registry, Type[] genericArguments)
        {
            Registry = registry;
            GenericArguments = genericArguments;
        }

        // 实现IEquatable接口，定义类型的相等性比较
        public bool Equals(Key? other)
        {
            //是否为相同的服务注册
            if (Registry != other?.Registry)
            {
                return false;
            }
            //依赖服务数量长度是否一致
            if (GenericArguments.Length != other.GenericArguments.Length)
            {
                return false;
            }
            //每项依赖的服务是否都相同
            for (int index = 0; index < GenericArguments.Length; index++)
            {
                if (GenericArguments[index] != other.GenericArguments[index])
                {
                    return false;
                }
            }
            return true;
        }
        
        // 重写 GetHashCode 方法以保持一致性
        public override int GetHashCode()
        {
            var hashCode = Registry.GetHashCode();
            //合并多个哈希码
            for (int index = 0; index < GenericArguments.Length; index++)
            {
                hashCode ^= GenericArguments[index].GetHashCode();
            }
            return hashCode;
        }

        // 重写 Equals 方法以满足 Object 类型的要求
        public override bool Equals(object? obj) => obj is Key key ? Equals(key) : false;
    }

}