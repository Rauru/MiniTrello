using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniTrello.Domain.Entities;

namespace MiniTrello.Api.Models
{
    public class OrganizationModel : Controller
    {
        //
        // GET: /OrganizationModel/

        private readonly IList<Account> _members = new List<Account>();
        private readonly IList<Board> _lanes = new List<Board>();

        //public virtual irtual Account Admind { get; set; }
        public virtual string Title { get; set; }
        public virtual Account OrganizationAdminAccount { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}
