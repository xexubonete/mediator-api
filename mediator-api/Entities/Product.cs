namespace webapi_docker.Entities
{
    /// <summary>
    /// Representa una entidad de producto en el sistema
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Obtiene o establece el identificador único del producto
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del producto
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Obtiene o establece el precio del producto
        /// </summary>
        public double? Price { get; set; }
    }
}
