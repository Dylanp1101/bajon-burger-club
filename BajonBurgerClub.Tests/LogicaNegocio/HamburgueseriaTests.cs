using BajonBurgerClub.LogicaNegocio.Entidades;
using BajonBurgerClub.LogicaNegocio.Excepciones;
using BajonBurgerClub.LogicaNegocio.ValueObjects;

namespace BajonBurgerClub.Tests.LogicaNegocio
{
    public class HamburgueseriaTests
    {
        private static Ubicacion UbicacionPocitos()
        {
            return new Ubicacion("Av. Brasil 2500", "Pocitos", "Montevideo", -34.911, -56.150);
        }

        private static Resena ResenaValida()
        {
            return new Resena(DateTime.Today, "Clásica", "Muy rica.", "", 390,
                new Puntaje(8), new Puntaje(8), new Puntaje(8), new Puntaje(8), new Puntaje(8));
        }

        [Fact]
        public void Crear_SinResena_NoEstaProbada()
        {
            Hamburgueseria local = new Hamburgueseria("La Bajonera", "@labajonera", UbicacionPocitos());

            Assert.False(local.EstaProbada);
            Assert.Equal("labajonera", local.Instagram);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Crear_SinNombre_LanzaExcepcion(string nombre)
        {
            Assert.Throws<HamburgueseriaInvalidaException>(() => new Hamburgueseria(nombre, "", UbicacionPocitos()));
        }

        [Fact]
        public void Crear_SinUbicacion_LanzaExcepcion()
        {
            Assert.Throws<HamburgueseriaInvalidaException>(() => new Hamburgueseria("La Bajonera", "", null!));
        }

        [Fact]
        public void Calificar_ConResena_QuedaProbada()
        {
            Hamburgueseria local = new Hamburgueseria("La Bajonera", "", UbicacionPocitos());

            local.Calificar(ResenaValida());

            Assert.True(local.EstaProbada);
            Assert.Equal(8.0m, local.Resena!.NotaFinal);
        }
    }
}
