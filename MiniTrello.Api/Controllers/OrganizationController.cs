using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AttributeRouting.Web.Http;
using AutoMapper;
using MiniTrello.Domain.Entities;
using MiniTrello.Domain.Services;

namespace MiniTrello.Api.Controllers
{
    public class OrganizationController : Controller
    {
        //
        // GET: /Organization/
       // public class OrganizationController(IReadOnlyRepository readOnlyRepository)
    //{
            
        
    //}
        readonly IReadOnlyRepository _readOnlyRepository;
        readonly IWriteOnlyRepository _writeOnlyRepository;
        readonly IMappingEngine _mappingEngine;

        public OrganizationController(IReadOnlyRepository readOnlyRepository, IWriteOnlyRepository writeOnlyRepository, IMappingEngine mappingEngine)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mappingEngine = mappingEngine;
        }

        
        [System.Web.Mvc.AcceptVerbs(new []{"DELETE"})]
        [DELETE("organization/{accessToken}")]
        public OrganizationModel Archive(string accessToken, [FromBody] cardArchiveModel model)
        {
            var organization = _readOnlyRepository.GetById<Organization>(model.Id);
            var archivedOrganization = _writeOnlyRepository.Archive(organization);
            return _mappingEngine.Map<Organization, OrganizationModel>(archivedOrganization);
        }

        [GET("organization/{accessToken}/{organizationId}")]
        public OrganizationModel GetById(string accessToken, long organizationId)
        {
            var organization = _readOnlyRepository.GetById<Organization>(organizationId);
            return _mappingEngine.Map<Organization, OrganizationModel>(organization);
        }

        [POST("/addorganization/{accessToken}")]
        public HttpResponseMessage Addorganization([FromBody] OrganizationRegisterModel model )
        {

            Organization organization = _mappingEngine.Map<OrganizationRegisterModel, Organization>(model);
            Organization createdOrganization = _writeOnlyRepository.Create(organization);
            if (createdOrganization != null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            throw new BadRequestException("Hubo un error al guardar el Board");
        }


    }

    public class OrganizationGetModel
    {
        public long Id { get; set; }
    }

    public class OrganizationModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsArchived { get; set; }
    }

    public class cardArchiveModel
    {
        public long Id { get; set; }
    }

    public class OrganizationRegisterModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }

}


