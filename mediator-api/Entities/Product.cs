namespace webapi_docker.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}
