using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Lane : IEntity
    {
        private readonly IList<Board> _lanes = new List<Board>();
        

        //public virtual irtual Account Admind { get; set; }
        public virtual string Title { get; set; }
        public virtual Board BoardAccount { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }

        public virtual IEnumerable<Board> Members { get { return _lanes; } }
        public virtual void Addlane(Board lane)
        {
            if (!_lanes.Contains(lane))
            {
                _lanes.Add(lane);
            }
        }
    }

}
