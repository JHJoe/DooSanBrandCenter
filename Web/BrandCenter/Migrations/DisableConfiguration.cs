using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Common;
using System.Data.SqlClient;

namespace BrandCenter.Migrations
{

    public class DisableConfiguration : DbConfiguration
    {
        public DisableConfiguration()
        {
            SetDatabaseInitializer<DbContext>(null);
            SetManifestTokenResolver(new CustomManifestTokenResolver());
        }

        internal class CustomManifestTokenResolver : IManifestTokenResolver
        {
            private readonly IManifestTokenResolver _defaultResolver = new DefaultManifestTokenResolver();

            public string ResolveManifestToken(DbConnection connection)
            {
                var sqlConn = connection as SqlConnection;
                //for SQLServer, 2008 or 2012
                return sqlConn != null ? "2012" : _defaultResolver.ResolveManifestToken(connection);
            }
        }
    }

}