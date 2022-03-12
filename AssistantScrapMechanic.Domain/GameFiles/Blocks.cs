using System.Collections.Generic;

namespace AssistantScrapMechanic.Domain.GameFiles
{
    public class Blocklist
    {
        public List<Blocks> BlockList { get; set; }
    }

    public class Blocks
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        //public string Dif { get; set; }
        //public string Asg { get; set; }
        //public string Nor { get; set; }
        public string Tiling { get; set; }
        public string Color { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public bool Flammable { get; set; }
        //public decimal Density { get; set; }
        //public int QualityLevel { get; set; }
        //public Restrictions Restrictions { get; set; }
        //public bool Glass { get; set; }
        //public bool Alpha { get; set; }
    }

    //public class Restrictions
    //{
    //    public bool Destructable { get; set; }
    //    public bool Erasable { get; set; }
    //    public bool Liftable { get; set; }
    //    public bool Paintable { get; set; }
    //}

}
