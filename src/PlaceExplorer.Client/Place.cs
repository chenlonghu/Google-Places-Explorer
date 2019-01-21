namespace PlaceExplorer.Client
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Place
    {
        public string Place_id { get; set; }
        public string Name { get; set; }
        public string Vicinity { get; set; }
        public Geometry Geometry { get; set; }
        //public OpeningHour Opening_hours { get; set; }
        //public double Rating { get; set; }
        public string[] Types { get; set; }
    }
}
