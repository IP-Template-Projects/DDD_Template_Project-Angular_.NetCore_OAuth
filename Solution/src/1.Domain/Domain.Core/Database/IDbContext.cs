﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Core.Database
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}