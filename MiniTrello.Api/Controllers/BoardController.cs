using System.Collections;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    //[Post/put("boards/addmember/{accesstoken}")]
    //httpsresponcemessage
    public class BoardController : Controller
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public BoardController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository,
            IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        [PUT("boards/addmember/{accessToken}")]
        public BoardModel AddMember([FromBody] AddMeberBoardModel model, string accessToken)
        {
            //validar seguridad

            var memberToAdd = _readOnlyRepository.GetById<Account>(model.MemberId);
            var board = _readOnlyRepository.GetById<Board>(model.BoardId);

            board.AddMember(memberToAdd);
            var updatedBoard = _writeOnlyRepository.Update(board);
            var boardModel = _mappingEngine.Map<Board, BoardModel>(updatedBoard);
            return boardModel;
        }

        [POST("/addboard/{accessToken}")]
        public HttpResponseMessage Addboard([FromBody] BoardRegisterModel model)
        {

            Board board = _mappingEngine.Map<BoardRegisterModel, Board>(model);
            Board createdBoard = _writeOnlyRepository.Create(board);
            if (createdBoard != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el Board");
        }

        [PUT("/changename/{accesstoken}")]
        public HttpResponseMessage Changename ([FromBody] BoardModel model)
        {
            Board board = _mappingEngine.Map<BoardModel, Board>(model);
            Board updatedBoard = _writeOnlyRepository.Update(board);
            if (updatedBoard != null )
            {
                return new HttpResponseMessage(HttpStatusCode.OK);   
            }
            throw  new BadRequestException("Hubo un Error al Cambiar el Nombre");
        }

        [GET("members/{accesstoken}")]
        public IEnumerable Membersview([FromBody] Board model)
        {

            Board board = _mappingEngine.Map<Board, Board>(model);
            Board boardmembers = _readOnlyRepository.GetById<Board>(board.Id);
            
            IEnumerable members = boardmembers.Members;
            return members;
            
        }

        [System.Web.Mvc.AcceptVerbs(new[] { "DELETE" })]
        [DELETE("boards/{accessToken}")]
        public BoardModel Archive(string accessToken, [FromBody] cardArchiveModel model)
        {
            var board = _readOnlyRepository.GetById<Board>(model.Id);
            var archivedBoard = _writeOnlyRepository.Archive(board);
            return _mappingEngine.Map<Board, BoardModel>(archivedBoard);
        }

        public class BoardRegisterModel
        {
            public string Title { get; set; }
            public int Id { get; set; }
        }

        public class AddMeberBoardModel
        {
            public long MemberId { get; set; }
            public long BoardId { get; set; }
        }
    }

    public class BoardModel
    {
        public string Title { get; set; }
    }

    public class BadRequestException : HttpResponseException
    {
        public BadRequestException(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }

        public BadRequestException(HttpResponseMessage response)
            : base(response)
        {
        }

        public BadRequestException(string errorMessage)
            : base(HttpStatusCode.BadRequest)
        {

            this.Response.ReasonPhrase = errorMessage;
        }
    }
}
