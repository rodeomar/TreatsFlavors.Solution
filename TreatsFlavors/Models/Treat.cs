namespace TreatsFlavors.Models
{
    public class Treat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TreatFlavor> TreatFlavors { get; set; }

    }
}
