using System.Collections.Generic;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Domain.GameFiles
{
    public class GameItemlist
    {
        [JsonProperty("partList")]
        public List<GameItem> GameItemList { get; set; }
    }

    public class GameItem
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        //public string Renderable { get; set; }
        public string Color { get; set; }
        //public Cylinder Cylinder { get; set; }
        //public string RotationSet { get; set; }
        //public string Sticky { get; set; }
        public string PhysicsMaterial { get; set; }
        public Ratings Ratings { get; set; }
        public Box Box { get; set; }
        public bool Flammable { get; set; }
        public decimal Density { get; set; }
        public int StackSize { get; set; }
    }

    //public class Cylinder
    //{
    //    public int Diameter { get; set; }
    //    public int Depth { get; set; }
    //    public float Margin { get; set; }
    //    public string Axis { get; set; }
    //}

    public class Box
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

}
