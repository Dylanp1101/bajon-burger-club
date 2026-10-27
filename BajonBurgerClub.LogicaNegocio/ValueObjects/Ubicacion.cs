using BajonBurgerClub.LogicaNegocio.Excepciones;

namespace BajonBurgerClub.LogicaNegocio.ValueObjects
{
    // Dónde queda una hamburguesería: dirección para mostrar y coordenadas para el mapa.
    public record Ubicacion
    {
        public static readonly string[] Departamentos =
        {
            "Artigas", "Canelones", "Cerro Largo", "Colonia", "Durazno", "Flores", "Florida",
            "Lavalleja", "Maldonado", "Montevideo", "Paysandú", "Río Negro", "Rivera", "Rocha",
            "Salto", "San José", "Soriano", "Tacuarembó", "Treinta y Tres"
        };

        // Rectángulo aproximado que contiene a Uruguay.
        private const double LatitudMinima = -35.2;
        private const double LatitudMaxima = -30.0;
        private const double LongitudMinima = -58.6;
        private const double LongitudMaxima = -53.0;

        public string Direccion { get; }
        public string Barrio { get; }
        public string Departamento { get; }
        public double Latitud { get; }
        public double Longitud { get; }

        public Ubicacion(string direccion, string barrio, string departamento, double latitud, double longitud)
        {
            if (string.IsNullOrWhiteSpace(direccion))
            {
                throw new UbicacionInvalidaException("La dirección es obligatoria.");
            }
            if (!Departamentos.Contains(departamento))
            {
                throw new UbicacionInvalidaException($"'{departamento}' no es un departamento de Uruguay.");
            }
            if (latitud < LatitudMinima || latitud > LatitudMaxima || longitud < LongitudMinima || longitud > LongitudMaxima)
            {
                throw new UbicacionInvalidaException("Las coordenadas no están dentro de Uruguay.");
            }

            Direccion = direccion.Trim();
            Barrio = barrio?.Trim() ?? "";
            Departamento = departamento;
            Latitud = latitud;
            Longitud = longitud;
        }
    }
}
