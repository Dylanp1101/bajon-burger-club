# 🍔 Bajón Burger Club

> **bajón** *(rioplatense)*: antojo repentino e incontrolable de comida rica.

El top de hamburguesas de Uruguay, probadas en carne propia. Cada hamburguesería tiene mi reseña y una nota por criterio (pan, carne, papas, sabor y relación precio/calidad), un ranking general y un mapa para llegar con Google Maps o Waze.

> 🚧 En construcción.

## Stack

- **ASP.NET Core MVC** (.NET 10) + **Entity Framework Core**
- Arquitectura en capas (dominio, casos de uso, acceso a datos, web)
- **Tailwind CSS** + **Motion** para el diseño y las animaciones
- **React + TypeScript** solo en las partes interactivas (mapa y filtros)
- **xUnit** para los tests y **GitHub Actions** para la integración continua

## Estructura

```
BajonBurgerClub.LogicaNegocio     Dominio: entidades, value objects, reglas y excepciones
BajonBurgerClub.LogicaAplicacion  Casos de uso, DTOs y mappers
BajonBurgerClub.AccesoDatos       EF Core: DbContext, repositorios y migraciones
BajonBurgerClub.WebApp            MVC: controllers, vistas y front
BajonBurgerClub.Tests             Tests del dominio y de los casos de uso
```

Las dependencias van siempre hacia el dominio: `WebApp → LogicaAplicacion → LogicaNegocio ← AccesoDatos`.

## Correr el proyecto

```bash
dotnet build
dotnet test
dotnet run --project BajonBurgerClub.WebApp
```

## Licencia

[MIT](LICENSE) © 2026 Dylan Pires
