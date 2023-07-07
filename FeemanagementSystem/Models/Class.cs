using System;
using System.Collections.Generic;

namespace FeemanagementSystem.Models;

public partial class Class
{
    public int Cid { get; set; }

    public string Cname { get; set; } = null!;

    public virtual ICollection<FeeStructure> FeeStructures { get; set; } = new List<FeeStructure>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
