using System.Collections.Generic;
using System.Linq;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MiniTrello.Domain.Entities;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace MiniTrello.DatabaseDeployer
{
    public class AccountSeeder : IDataSeeder
    {
        readonly ISession _session;

        public AccountSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            IList<Account> accountList = Builder<Account>.CreateListOfSize(10).Build();
            accountList.ElementAt(0).Email = "raul_lopez_tu@hotmail.com";
            accountList.ElementAt(0).Password = "ghjtyuio";

            foreach (Account account in accountList)
            {
                var boards = Builder<Board>.CreateListOfSize(2).Build();
                foreach (var board in boards)
                {
                    _session.Save(board);
                }
                account.AddBoard(boards[0]);
                account.AddBoard(boards[1]);
                _session.Save(account);
            }
        }

       /* public void CreateDatabaseSchemaFromMappingFiles()
        {
            FluentConfiguration config = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString("......"))
            .Mappings(
                m => m.FluentMappings.Add(typeof(Account)
            ));

            config.ExposeConfiguration(
                      c => new SchemaExport(c).Execute(true, true, false))
                 .BuildConfiguration();
        }*/
    }
}