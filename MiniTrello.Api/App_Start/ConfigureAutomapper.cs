using System;
using System.Data.Entity;
using AutoMapper;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Infrastructure;

namespace MiniTrello.Api
{
    public class ConfigureAutomapper : IBootstrapperTask
    {
        public void Run()
        {
            Mapper.CreateMap<Account, AccountLoginModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountRegisterModel>().ReverseMap();
            //Mapper.CreateMap<DemographicsEntity, DemographicsModel>().ReverseMap();
            //Mapper.CreateMap<IReportEntity, IReportModel>()
            //    .Include<DemographicsEntity, DemographicsModel>();
           /* Database.SetInitializer<Account>(new DropCreateDatabaseIfModelChanges<AccountRegisterModel>());
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(aClassFromYourProject).Assembly);
            new SchemaExport(cfg).Execute(false, true, false, false); */
            
        }
    }
}