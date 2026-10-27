using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.InterfacesEntidades;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.LogicaNegocio.Entidades
{
    // Un local del mapa. Puede estar sin probar (sin reseña) o calificado.
    public class Hamburgueseria : IValidable
    {
        public const int LargoMaximoNombre = 80;

        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Instagram { get; private set; }
        public Ubicacion Ubicacion { get; private set; }
        public Resena? Resena { get; private set; }

        public bool EstaProbada
        {
            get { return Resena is not null; }
        }

        public Hamburgueseria(string nombre, string instagram, Ubicacion ubicacion)
        {
            Nombre = nombre?.Trim() ?? "";
            Instagram = instagram?.Trim().TrimStart('@') ?? "";
            Ubicacion = ubicacion;
            Validar();
        }

        // Lo usa EF Core para reconstruir el objeto desde la base: él completa las propiedades después.
#pragma warning disable CS8618
        private Hamburgueseria()
        {
        }
#pragma warning restore CS8618

        // Si vuelvo a ir y cambia mi opinión, la reseña nueva reemplaza a la anterior.
        public void Calificar(Resena resena)
        {
            if (resena is null)
            {
                throw new HamburgueseriaInvalidaException("La reseña no puede ser vacía.");
            }
            Resena = resena;
        }

        public void Validar()
        {
            if (Nombre == "")
            {
                throw new HamburgueseriaInvalidaException("El nombre es obligatorio.");
            }
            if (Nombre.Length > LargoMaximoNombre)
            {
                throw new HamburgueseriaInvalidaException($"El nombre no puede pasar de {LargoMaximoNombre} caracteres.");
            }
            if (Ubicacion is null)
            {
                throw new HamburgueseriaInvalidaException("La ubicación es obligatoria.");
            }
        }
    }
}
