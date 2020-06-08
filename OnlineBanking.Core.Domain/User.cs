using System;

namespace OnlineBanking.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
       
        public Account Account { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

    }
}
