﻿using Domain.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        int GetMaxOrder(long routeId);
    }
}
