using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyanDo.Entity
{
    public class FlyOwner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PicturePath { get; set; }
        public virtual ICollection<Fly> Flys { get; set; }
    }
}
