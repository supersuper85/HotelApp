﻿using AuditApp.Data.Entities.Context;


namespace AuditApp.Data.Implementations
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
