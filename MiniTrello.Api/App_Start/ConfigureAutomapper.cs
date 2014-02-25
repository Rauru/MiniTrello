using System;
using System.Data.Entity;
using AutoMapper;
using MiniTrello.Api.Controllers;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Infrastructure;
using OrganizationModel = MiniTrello.Api.Models.OrganizationModel;

namespace MiniTrello.Api
{
    public class ConfigureAutomapper : IBootstrapperTask
    {
        public void Run()
        {
            Mapper.CreateMap<Account, AccountLoginModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountRegisterModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountLoginModel>().ReverseMap();
            Mapper.CreateMap<Account, AccountRegisterModel>().ReverseMap();
            Mapper.CreateMap<Organization, OrganizationModel>().ReverseMap();
            Mapper.CreateMap<Board, BoardModel>().ReverseMap();
            //Mapper.CreateMap<Lane, LaneModel>().ReverseMap();
            /*Mapper.CreateMap<Card, CardModel>().ReverseMap();
            Mapper.CreateMap<CreateBoardModel, Board>().ReverseMap();
            Mapper.CreateMap<CreateCardModel, Card>().ReverseMap();
            Mapper.CreateMap<CreateLaneModel, Lane>().ReverseMap();*/
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