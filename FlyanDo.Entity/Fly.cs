using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyanDo.Entity
{
    public class Fly
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual FlyOwner Owner { get; set; }
        public DateTime DateOfFly { get; set; }
        public virtual ICollection<FlyComment> Comments { get; set; }
    }
}
