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
                _session.Save(account);
                //IList<Board> boards = Builder<Board>.CreateListOfSize(3).Build();
                var boards = Builder<Board>.CreateListOfSize(2).Build();
                IList<Organization> organizations = Builder<Organization>.CreateListOfSize(2).Build();
                foreach (var board in boards)
                {
                    IList<Lane> lanes = Builder<Lane>.CreateListOfSize(2).Build();
                    foreach (var lane in lanes)
                    {
                       IList<Card> cards = Builder<Card>.CreateListOfSize(4).Build();
                        foreach (var card in cards)
                        {
                            _session.Save(card);
                            lane.Addcard(card);
                        }

                        _session.Save(lane);
                        board.addlane(lane);
                    }

                    board.AddMember(account);
                    _session.Save(board);
                }
                foreach (var organization in organizations)
                {
                    _session.Save(organization);
                }

                
                account.AddBoard(boards[0]);
                account.AddBoard(boards[1]);
                _session.Update(account);
               
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