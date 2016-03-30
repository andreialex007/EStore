using System;
using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;

namespace EStore.BL.Models
{
    public class UserItem : ViewModelBase
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual bool? IsAdmin { get; }
    }

    public class AdminUserItem : UserItem
    {
        public override bool? IsAdmin => true;
    }
}