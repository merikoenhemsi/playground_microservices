﻿namespace Ordering.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; protected set; }
    public DateTime CreatedDate { get;  set; }
    public DateTime? ModifiedDate { get;  set; }
}