namespace Fop
{
   public class FopQuery : IFopQuery
    {
        public string Filter { get; set; }

        public string Order { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
