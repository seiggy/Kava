using System;

namespace KavaAPI
{
    public class User
    {
        public Guid Pkey { get; private set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}