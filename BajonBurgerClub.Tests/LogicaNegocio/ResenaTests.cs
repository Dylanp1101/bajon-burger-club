using BajonBurgerClub.LogicaNegocio.Entidades;
using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.Tests.LogicaNegocio
{
    public class ResenaTests
    {
        // Arma una reseña válida; cada test cambia solo lo que quiere probar.
        private static Resena CrearResena(
            string comentario = "Medallón jugoso y pan brioche de 10.",
            decimal precio = 450,
            DateTime? fecha = null,
            int sabor = 9, int carne = 9, int precioCalidad = 7, int pan = 8, int papas = 6)
        {
            return new Resena(fecha ?? DateTime.Today, "Doble cheddar", comentario, "Texto largo de la reseña.", precio,
                new Puntaje(sabor), new Puntaje(carne), new Puntaje(precioCalidad), new Puntaje(pan), new Puntaje(papas));
        }

        [Fact]
        public void NotaFinal_ConTodoEn10_Da10()
        {
            Resena resena = CrearResena(sabor: 10, carne: 10, precioCalidad: 10, pan: 10, papas: 10);

            Assert.Equal(10.0m, resena.NotaFinal);
        }

        [Fact]
        public void NotaFinal_PonderaCadaCriterioYRedondeaAUnDecimal()
        {
            // 9*0.30 + 9*0.25 + 7*0.20 + 8*0.15 + 6*0.10 = 8.15 -> 8.2
            Resena resena = CrearResena();

            Assert.Equal(8.2m, resena.NotaFinal);
        }

        [Fact]
        public void Crear_ConComentarioDeMasDe140Caracteres_LanzaExcepcion()
        {
            string comentarioLargo = new string('a', 141);

            Assert.Throws<ResenaInvalidaException>(() => CrearResena(comentario: comentarioLargo));
        }

        [Fact]
        public void Crear_SinComentario_LanzaExcepcion()
        {
            Assert.Throws<ResenaInvalidaException>(() => CrearResena(comentario: "   "));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Crear_ConPrecioNoPositivo_LanzaExcepcion(decimal precio)
        {
            Assert.Throws<ResenaInvalidaException>(() => CrearResena(precio: precio));
        }

        [Fact]
        public void Crear_ConFechaFutura_LanzaExcepcion()
        {
            Assert.Throws<ResenaInvalidaException>(() => CrearResena(fecha: DateTime.Today.AddDays(1)));
        }
    }
}
