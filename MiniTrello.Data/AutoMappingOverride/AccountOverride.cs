using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Data.AutoMappingOverride
{
    public class AccountOverride : IAutoMappingOverride<Account>
    {
        public void Override(AutoMapping<Account> mapping)
        {
             //mapping.HasMany(x => x.Referrals)
             //    .Inverse()
             //    .Access.CamelCaseField(Prefix.Underscore);

            /////////////////////////////////////////////////////////////////////////
            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Account).Assembly);
            new NHibernate.Tool.hbm2ddl.SchemaExport(cfg).Execute(false, true, false);
            /////////////////////////////////////////////////////////////////////////
        }
    }
}
