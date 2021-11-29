using System;

namespace _0_Framework
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreationTime = DateTime.Now;
        }

        public int Id { get; private set; }
        public DateTime CreationTime { get; private set; }
    }
}
