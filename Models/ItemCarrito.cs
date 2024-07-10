namespace Libreria.Models;

public class ItemCarrito
{
    public int Id { get; set; }
    public int CarritoId { get; set; }
    public int LibroId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    public CarritoDeCompra Carrito { get; set; }
    public Libro Libro { get; set; }
}
