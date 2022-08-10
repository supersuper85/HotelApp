using HotelApp.Data.Entities;
using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace HotelApp.Data.Implementations
{
    public class DefaultRepository<T> :  BaseEntityFrameworkRepository<T> where T : class
    {

        public DefaultRepository(DataBaseContext context) : base(context)
        {
        }

        protected override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

    }
}
