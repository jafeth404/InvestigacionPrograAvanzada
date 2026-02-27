namespace InvoiceManager.Models;

public class Customer
{
    
    public int Id { get; set; }          // Identificador único
    public string Nombre { get; set; }     // Nombre del cliente
    public string Email { get; set; }    // Correo
    public string Telefono { get; set; }    // Teléfono
    public string Direccion { get; set; }  // Dirección física
}