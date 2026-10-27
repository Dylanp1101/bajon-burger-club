namespace BajonBurgerClub.LogicaNegocio.Excepciones
{
    // Base de todas las excepciones del dominio: permite atraparlas juntas en la web.
    public class DominioException : Exception
    {
        public DominioException(string mensaje) : base(mensaje)
        {
        }
    }
}
