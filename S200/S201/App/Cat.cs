using System.Collections.Concurrent;

namespace App
{
    public class Cat : IServiceProvider, IDisposable
    {
        //根容器对象
        internal readonly Cat _root;
        //添加的所有服务注册
        internal readonly ConcurrentDictionary<Type, ServiceRegistry> _registries;
        //除Transient外的所有服务实例
        private readonly ConcurrentDictionary<Key, object?> _services;
        //待释放列表
        private readonly ConcurrentBag<IDisposable> _disposables;
        private volatile bool _disposed;

        public Cat()
        {
            _registries = new ConcurrentDictionary<Type, ServiceRegistry>();
            _root = this; //自指
            _services = new ConcurrentDictionary<Key, object?>();
            _disposables = new ConcurrentBag<IDisposable>();
        }

        internal Cat(Cat parent)
        {
            _root = parent._root;
            _registries = _root._registries; //指向根容器的服务注册集
            _services = new ConcurrentDictionary<Key, object?>();
            _disposables = new ConcurrentBag<IDisposable>();
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Cat");
            }
        }

        //所有服务注册扩展方法的基础方法
        public Cat Register(ServiceRegistry registry)
        {
            EnsureNotDisposed();
            //查找该服务类型的服务注册，找到后添加到服务注册链表中
            if (_registries.TryGetValue(registry.ServiceType, out var existing))
            {
                _registries[registry.ServiceType] = registry;
                registry.Next = existing;
            }
            else
            {
                //没有找到就直接添加为新的服务注册链表
                _registries[registry.ServiceType] = registry;
            }
            return this;
        }

        public object? GetService(Type serviceType)
        {
            EnsureNotDisposed();
            //需要Cat类或IServiceProvider类服务时返回自身
            if (serviceType == typeof(Cat) || serviceType == typeof(IServiceProvider))
            {
                return this;
            }

            //服务注册
            ServiceRegistry? registry;
            //IEnumerable<T> 可枚举服务
            if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elementType = serviceType.GetGenericArguments()[0];
                if (!_registries.TryGetValue(elementType, out registry))
                {
                    return Array.CreateInstance(elementType, 0);
                }

                var registries = registry.AsEnumerable();
                var services = registries.Select(it => GetServiceCore(it,
                    Type.EmptyTypes)).Reverse().ToArray();
                Array array = Array.CreateInstance(elementType, services.Length);
                services.CopyTo(array, 0);
                return array;
            }

            //Generic 带有泛型参数列表的服务
            if (serviceType.IsGenericType && !_registries.ContainsKey(serviceType))
            {
                var definition = serviceType.GetGenericTypeDefinition();
                return _registries.TryGetValue(definition, out registry)
                    ? GetServiceCore(registry, serviceType.GetGenericArguments())
                    : null;
            }

            //Normal 普通服务
            return _registries.TryGetValue(serviceType, out registry)
                    ? GetServiceCore(registry, new Type[0])
                    : null;
        }

        public void Dispose()
        {
            _disposed = true;
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
            _services.Clear();
        }

        //所有提供服务实例的方法的基础方法，提供服务注册和依赖服务类型列表，返回一个服务实例
        private object? GetServiceCore(ServiceRegistry registry, Type[] genericArguments)
        {
            var key = new Key(registry, genericArguments);
            var serviceType = registry.ServiceType;

            switch (registry.Lifetime)
            {
                case Lifetime.Root: return GetOrCreate(_root._services, _root._disposables);
                case Lifetime.Self: return GetOrCreate(_services, _disposables);
                default:
                    {
                        //Transmit生命周期的服务直接创建对象
                        var service = registry.Factory(this, genericArguments);

                        //实现了IDisposable接口的对象会被放入待释放列表中
                        //is IDisposable disposable 是一个模式匹配的语法，它尝试将 service 对象转换为实现了 IDisposable 接口的对象，并将转换后的对象赋值给变量 disposable。
                        //如果 service 实现了 IDisposable 接口，那么 disposable 就会引用这个对象，否则为 null。
                        if (service is IDisposable disposable && disposable != this)
                        {
                            _disposables.Add(disposable);
                        }
                        return service;
                    }
            }

            //嵌套方法
            object? GetOrCreate(ConcurrentDictionary<Key, object?> services, ConcurrentBag<IDisposable> disposables)
            {
                if (services.TryGetValue(key, out var service))
                {
                    return service;
                }
                service = registry.Factory(this, genericArguments);
                services[key] = service;
                //实现了IDisposable接口的对象会被放入待释放列表中
                if (service is IDisposable disposable)
                {
                    disposables.Add(disposable);
                }
                return service;
            }
        }
    }
}