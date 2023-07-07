using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class Teacher
{
    public int Tid { get; set; }

    public int UserId { get; set; }

    public string Tpost { get; set; } = null!;

    public virtual UserList User { get; set; } = null!;
}
