using System;

namespace _0_Framework
{
    public class BaseEntity<T>
    {
        public BaseEntity()
        {
            CreationTime = DateTime.Now;
        }

        public T Id { get; private set; }
        public DateTime CreationTime { get; private set; }
    }
}
