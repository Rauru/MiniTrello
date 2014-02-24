using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Api.Models;
using System.Collections;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using MiniTrello.Api.Controllers;

namespace MiniTrello.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private readonly IWriteOnlyRepository _writeOnlyRepository;
        private readonly IMappingEngine _mappingEngine;

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
           // Account account0;
           /* Account account =
                _readOnlyRepository.Getbyemail<Account>( account0.Email == model.Email && account0.Password == model.Password);*/

            if (account != null)
            {
                TokenController tokenizer = new TokenController();
               // token.Createtoken(account.Email);
                //return new AuthenticationModel() {Token = "Existe"};
                AuthenticationModel token =  new AuthenticationModel();
                token.Token = tokenizer.Createtoken(account.Email);
                string tokenval = tokenizer.Createtoken(account.Email);
                return new AuthenticationModel() {Token = token.Token};
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
            
            //return id.ToString();
            Account account = _mappingEngine.Map<AccountRegisterModel, Account>(model);
            //account.Id = token.Gettoken(account.FirstName);
            Account accountCreated = _writeOnlyRepository.Create(account);

            
           
            if (accountCreated != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el usuario");
        }

        [PUT("edit")]
        public HttpResponseMessage EditProfile([FromBody] AccountUpdateModel model, string accessToken)
        {
            //var accountToedit = _readOnlyRepository.GetById<Account>(model.Id);
            Account accountToedit = _readOnlyRepository.GetByName<Account>(model.FirstName);
            Account account = _writeOnlyRepository.Update(accountToedit);
            if (account != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el usuario");
        }
        /*//public ActionResult Edit(int id)
        //{
          //  var model = _models.Get(id);
         //   return View("Create", model);
        //}*/
        public class AccountUpdateModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
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
}