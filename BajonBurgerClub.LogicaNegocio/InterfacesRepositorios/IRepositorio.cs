namespace BajonBurgerClub.LogicaNegocio.InterfacesRepositorios
{
    // Operaciones que comparten todos los repositorios. T es la entidad (Hamburgueseria, Resena...).
    public interface IRepositorio<T>
    {
        void Add(T obj);
        void Update(T obj);
        void Remove(int id);
        T? FindById(int id);
        IEnumerable<T> FindAll();
    }
}
