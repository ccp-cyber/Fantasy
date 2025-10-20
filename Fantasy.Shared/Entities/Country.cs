using System.ComponentModel.DataAnnotations;

namespace Fantasy.Shared.Entities;

public class Country
{
    public int Id { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;

    //Esta campo no es obligatorio ni existe en em MER, pero ayuda a identificar que un pais tiene muchos equipos
    public ICollection<Team> Teams { get; set; }

    //Propiedad de lectura: Esta no se mapea a la bbdd y se usa una operacion de flecha // el perador ternario "?" ayuda a controlar el error de null
    public int TeamsCount => Teams == null ? 0 : Teams.Count;
}