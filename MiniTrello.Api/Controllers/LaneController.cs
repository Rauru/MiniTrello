using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class LaneController : Controller
    {
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public LaneController(IWriteOnlyRepository writeOnlyRepository, IReadOnlyRepository readOnlyRepository, IMappingEngine mappingEngine)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _mappingEngine = mappingEngine;
        }

    }
}