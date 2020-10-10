﻿using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Data
{
    public class User : UserIdentity
    {

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<UserCard>();
        }
        public virtual ICollection<UserCard> Cards { get; set; }
    }
}
