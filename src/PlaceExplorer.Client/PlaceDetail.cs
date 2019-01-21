namespace PlaceExplorer.Client
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PlaceDetail : Place
    {
        public string International_phone_number { get; set; }
        public string Website { get; set; }
        public string Url { get; set; }
    }
}
