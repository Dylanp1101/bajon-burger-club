using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.InterfacesEntidades;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.LogicaNegocio.Entidades
{
    // Mi opinión sobre una hamburguesa que probé en un local.
    public class Resena : IValidable
    {
        public const int LargoMaximoComentario = 140;

        // Cuánto pesa cada criterio en la nota final (suman 1).
        public const decimal PesoSabor = 0.30m;
        public const decimal PesoCarne = 0.25m;
        public const decimal PesoPrecioCalidad = 0.20m;
        public const decimal PesoPan = 0.15m;
        public const decimal PesoPapas = 0.10m;

        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string HamburguesaProbada { get; private set; }
        public string ComentarioCorto { get; private set; }
        public string Texto { get; private set; }
        public decimal Precio { get; private set; }

        public Puntaje Sabor { get; private set; }
        public Puntaje Carne { get; private set; }
        public Puntaje PrecioCalidad { get; private set; }
        public Puntaje Pan { get; private set; }
        public Puntaje Papas { get; private set; }

        // No se guarda: se calcula cada vez a partir de los criterios.
        public decimal NotaFinal
        {
            get
            {
                decimal nota = Sabor.Valor * PesoSabor
                    + Carne.Valor * PesoCarne
                    + PrecioCalidad.Valor * PesoPrecioCalidad
                    + Pan.Valor * PesoPan
                    + Papas.Valor * PesoPapas;
                return Math.Round(nota, 1, MidpointRounding.AwayFromZero);
            }
        }

        public Resena(DateTime fecha, string hamburguesaProbada, string comentarioCorto, string texto, decimal precio,
            Puntaje sabor, Puntaje carne, Puntaje precioCalidad, Puntaje pan, Puntaje papas)
        {
            Fecha = fecha;
            HamburguesaProbada = hamburguesaProbada?.Trim() ?? "";
            ComentarioCorto = comentarioCorto?.Trim() ?? "";
            Texto = texto?.Trim() ?? "";
            Precio = precio;
            Sabor = sabor;
            Carne = carne;
            PrecioCalidad = precioCalidad;
            Pan = pan;
            Papas = papas;
            Validar();
        }

        // Lo usa EF Core para reconstruir el objeto desde la base: él completa las propiedades después.
#pragma warning disable CS8618
        private Resena()
        {
        }
#pragma warning restore CS8618

        public void Validar()
        {
            if (HamburguesaProbada == "")
            {
                throw new ResenaInvalidaException("Hay que indicar qué hamburguesa probaste.");
            }
            if (ComentarioCorto == "")
            {
                throw new ResenaInvalidaException("El comentario corto es obligatorio: es el que se ve en el mapa.");
            }
            if (ComentarioCorto.Length > LargoMaximoComentario)
            {
                throw new ResenaInvalidaException($"El comentario corto no puede pasar de {LargoMaximoComentario} caracteres.");
            }
            if (Precio <= 0)
            {
                throw new ResenaInvalidaException("El precio tiene que ser mayor a 0.");
            }
            if (Fecha.Date > DateTime.Today)
            {
                throw new ResenaInvalidaException("No podés reseñar algo que todavía no comiste.");
            }
            if (Sabor is null || Carne is null || PrecioCalidad is null || Pan is null || Papas is null)
            {
                throw new ResenaInvalidaException("Faltan puntajes: todos los criterios son obligatorios.");
            }
        }
    }
}
