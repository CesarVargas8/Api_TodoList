﻿using System;
using System.Collections.Generic;

namespace Api_TodoList.Models;

public partial class User
{
    public int Id { get; set; }

    public string User1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
