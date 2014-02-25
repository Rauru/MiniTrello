using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrello.Domain.Entities
{
    public class Lane : IEntity
    {
        private readonly IList<Card> _cards = new List<Card>();
        

        //public virtual irtual Account Admind { get; set; }
        public virtual string Title { get; set; }
        public virtual Board SetBoard { get; set; }
        public virtual long Id { get; set; }
        public virtual bool IsArchived { get; set; }

        public virtual void Addcard(Card card)
        {
            if (!_cards.Contains(card))
            {
                _cards.Add(card);
            }

        }
        
    }

}
