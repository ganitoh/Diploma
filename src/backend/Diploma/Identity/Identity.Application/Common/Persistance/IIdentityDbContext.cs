﻿using Identity.Domain.Models;

namespace Identity.Application.Common.Persistance;

public interface IIdentityDbContext
{
    IQueryable<User> Users { get; }
}