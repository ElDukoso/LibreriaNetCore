namespace Libreria.Models;

public class CarritoDeCompra
{
    public int Id { get; set; }
    public int? UsuarioId { get; set; }
    public decimal Total { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<ItemCarrito> Items { get; set; }
}