using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AttributeRouting.Web.Http;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;
using AutoMapper;

namespace MiniTrello.Api.Controllers
{
    public class CardController : Controller
    {
        //
        // GET: /Card/

        //public ActionResult Index()
        //{return View();}

        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public CardController(IWriteOnlyRepository writeOnlyRepository, IMappingEngine mappingEngine, IReadOnlyRepository readOnlyRepository)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
            _readOnlyRepository = readOnlyRepository;
        }

        [POST("/addcard/{accessToken}")]
        public HttpResponseMessage AddCard([FromBody] CardRegisterModel model)
        {

            Card card = _mappingEngine.Map<CardRegisterModel, Card>(model);
            Card createdBoard = _writeOnlyRepository.Create(card);


            if (createdBoard != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar la Card");
        }

        [PUT("/changecardname/{accesstoken}")]
        public HttpResponseMessage Changename([FromBody] CardModel model)
        {
            Card card = _mappingEngine.Map<CardModel, Card>(model);
            Card updatedCard = _writeOnlyRepository.Update(card);
            if (updatedCard != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un Error al Cambiar el Nombre");
        }

        [System.Web.Mvc.AcceptVerbs(new[] { "DELETE" })]
        [DELETE("cards/{accessToken}")]
        public CardModel Archive(string accessToken, [FromBody] cardArchiveModel model)
        {
            var card = _readOnlyRepository.GetById<Card>(model.Id);
            var archivedCard = _writeOnlyRepository.Archive(card);
            return _mappingEngine.Map<Card, CardModel>(archivedCard);
        }
    }

    public class cardArchivedModel
    {
        public long Id { get; set; }
    }

    public class CardRegisterModel
    {
            public string Title { get; set; }
            public int Id { get; set; }
    }
   
}
