using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyanDo.Entity
{
    public class FlyComment
    {
        public int Id { get; set; }
        public virtual Fly Fly { get; set; }
        public virtual FlyOwner Author { get; set; }
        public string Comment { get; set; }
    }
}
