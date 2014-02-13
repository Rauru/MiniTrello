using System;
using System.Collections.Generic;

namespace MiniTrello.Domain.Entities
{
    public class Board : IEntity
    {
        private readonly  IList<Account> _boards = new List<Account>();
        public virtual Account AccountAdmin { get; set; }
        //public virtual irtual Account Admind { get; set; }
        public virtual string Title { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }
        public virtual string Account_Admin { get; set; }

        //public virtual IEnumerable<Account> Members { get { return _} }
        public virtual void AddMember(Account member)
        {
            if(!_boards.Contains(member))
            {
                _boards.Add(member);
            }
        }
    }
}