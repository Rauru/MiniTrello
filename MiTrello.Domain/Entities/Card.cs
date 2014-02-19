using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Card : IEntity
    {
        private readonly IList<Lane> _lanes = new List<Lane>();
        public virtual string Title { get; set; }
        public virtual Lane BoardAccount { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
    }
}
