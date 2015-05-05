namespace CcAcca.BaseLibrary.DemoModel
{
    public class CostCentre
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public LookupItem Type { get; set; }
        public int TypeId { get; set; }
    }
}