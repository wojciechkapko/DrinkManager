namespace Domain
{
    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool DisallowManualChange { get; set; }
        public string Description { get; set; }
        public string FrontEndElementType { get; set; } = "input";
        public string AvailableOptions { get; set; }

    }
}
