namespace TreatsFlavors.Models
{
    public class Flavor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TreatFlavor> TreatFlavors { get; set; }


    }
}
