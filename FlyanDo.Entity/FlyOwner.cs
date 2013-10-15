using System.Collections.Generic;

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
