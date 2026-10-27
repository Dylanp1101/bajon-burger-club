using BajonBurgerClub.LogicaNegocio.Entidades;

namespace BajonBurgerClub.LogicaNegocio.InterfacesRepositorios
{
    // Lo que necesita el negocio de las hamburgueserías, además de lo común.
    public interface IRepositorioHamburgueseria : IRepositorio<Hamburgueseria>
    {
        // Solo las que ya tienen reseña: son las que entran al ranking.
        IEnumerable<Hamburgueseria> FindProbadas();

        // Para no cargar dos veces el mismo local en el mismo departamento.
        Hamburgueseria? FindByNombreYDepartamento(string nombre, string departamento);
    }
}
