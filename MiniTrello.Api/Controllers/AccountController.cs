using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Api.Models;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class AccountController : ApiController
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public AccountController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [POST("login")]
        public AuthenticationModel Login([FromBody] AccountLoginModel model)
        {
            var account =
                _readOnlyRepository.First<Account>(
                    account1 => account1.Email == model.Email && account1.Password == model.Password);
            if (account != null)
            {
                return new AuthenticationModel(){Token = "Existe"};
            }
            
            throw new BadRequestException(
                "Usuario o clave incorrecto");
        }

        [POST("register")]
        public HttpResponseMessage Register([FromBody] AccountRegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException("Claves no son iguales");
            }
            Account account = _mappingEngine.Map<AccountRegisterModel,Account>(model);
            Account accountCreated = _writeOnlyRepository.Create(account);
            if (accountCreated != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el usuario");
        }

        [POST("Edit")]
        public HttpResponseMessage EditProfile([FromBody] AccountRegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException("Claves no son iguales");
            }
            Account account = _mappingEngine.Map<AccountRegisterModel, Account>(model);
            Account accountCreated = _writeOnlyRepository.Update(account);
            if (accountCreated != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el usuario");
        }

        
        // Terminar
       // [POST("Invite")]
        //public HttpResponseMessage Invite([FromBody] InviteModel model)
        //{
       
          //  Account account = _mappingEngine.Map<InviteModel, Account>(model);
            //Board board = _mappingEngine.Map<BoardModel, Board>(model);

            //Account invitedAccount = _writeOnlyRepository.Create(account);
            //if (invitedAccount != null)
            //{
            //    return new HttpResponseMessage(HttpStatusCode.OK);
            //}
            //throw new BadRequestException("Hubo un error al Invitar al usuario");
        //}
    //}

    public class BadRequestException : HttpResponseException
    {
        public BadRequestException(HttpStatusCode statusCode) : base(statusCode)
        {
        }

        public BadRequestException(HttpResponseMessage response) : base(response)
        {
        }

        public BadRequestException(string errorMessage) : base(HttpStatusCode.BadRequest)
        {
            
            this.Response.ReasonPhrase = errorMessage;
        }
    }

    public class InviteModel
    {
        public string FirstName { get; set; }
        public long Id { get; set; }
    }
}