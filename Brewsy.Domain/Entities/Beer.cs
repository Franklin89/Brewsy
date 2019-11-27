namespace Brewsy.Domain.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
