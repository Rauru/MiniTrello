using System;
using System.Collections.Generic;

namespace MiniTrello.Domain.Entities
{
    public class Board : IEntity
    {
        private readonly  IList<Account> _members = new List<Account>();
        private readonly IList<Lane> _lanes = new List<Lane>();
        
        //public virtual irtual Account Admind { get; set; }
        public virtual string Title { get; set; }
        public virtual Account BoardAccount { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        //public virtual string Account_Admin { get; set; }

        public virtual IEnumerable<Account> Members { get { return _members; } }
        public virtual void AddMember(Account member)
        {
            if(!_members.Contains(member))
            {
                _members.Add(member);
            }
        }

        public virtual void addlane(Lane lane)
        {
            if (!_lanes.Contains(lane))
            {
                _lanes.Add(lane);
            }

        }
    }
}