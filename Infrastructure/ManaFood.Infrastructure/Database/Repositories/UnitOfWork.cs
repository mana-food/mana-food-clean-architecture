﻿using ManaFood.Application.Interfaces;
using ManaFood.Infrastructure.Database.Context;

namespace ManaFood.Infrastructure.Database.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _applicationContext;

    public UnitOfWork(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task Commit(CancellationToken cancellationToken)
    {
        await _applicationContext.SaveChangesAsync(cancellationToken);
    }
}
