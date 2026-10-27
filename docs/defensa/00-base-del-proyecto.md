# Fase 0: base del proyecto

## Qué se hizo

- Una solución (`BajonBurgerClub.slnx`) con 5 proyectos, uno por capa más los tests.
- Las referencias entre proyectos, que definen quién puede usar a quién.
- Repo git con `.gitignore`, `README.md`, licencia MIT y un workflow de GitHub Actions.

## Las capas y sus referencias

```
WebApp ──► LogicaAplicacion ──► LogicaNegocio
   │                                 ▲
   └──────► AccesoDatos ─────────────┘
Tests ──► LogicaAplicacion, LogicaNegocio
```

| Proyecto | Tipo | Responsabilidad | Referencia a |
|---|---|---|---|
| LogicaNegocio | classlib | Entidades, reglas, excepciones e **interfaces** de repositorios | nadie |
| LogicaAplicacion | classlib | Casos de uso: orquestan el dominio | LogicaNegocio |
| AccesoDatos | classlib | EF Core: implementa las interfaces de repositorio | LogicaNegocio |
| WebApp | mvc | Controllers y vistas; arma todo en `Program.cs` | LogicaAplicacion, AccesoDatos |
| Tests | xunit | Prueba el dominio y los casos de uso | LogicaNegocio, LogicaAplicacion |

## Preguntas de defensa

**¿Por qué LogicaNegocio no referencia a nadie?**
Porque es el centro: las reglas del negocio (por ejemplo, que una nota va de 1 a 10) no deben depender de si uso SQL Server, una web o una consola. Si mañana cambio la base de datos, el dominio no se toca.

**Si AccesoDatos no es referenciado por LogicaAplicacion, ¿cómo hace un caso de uso para guardar?**
El dominio define la interfaz (`IRepositorioHamburgueseria`) y AccesoDatos la implementa. El caso de uso solo conoce la interfaz. En `Program.cs` de la WebApp se le dice a la inyección de dependencias qué implementación usar. Esto es la **inversión de dependencias** (la D de SOLID).

**¿Por qué WebApp referencia a AccesoDatos si no lo usa directamente?**
Solo para registrarlo en `Program.cs` (`AddScoped<IRepositorio..., Repositorio...>()` y el `DbContext`). Los controllers nunca usan repositorios: hablan con casos de uso.

**¿Qué es el `.slnx`?**
El formato nuevo de solución de .NET, en XML. Es lo mismo que el `.sln` de siempre pero legible.

**¿Qué hace el workflow de GitHub Actions?**
Cada vez que hago push a `main` o abro un pull request, una máquina de GitHub descarga el código, lo compila y corre los tests. Si algo se rompe, me avisa. Eso es **integración continua (CI)**.

**¿Por qué licencia MIT?**
Es la licencia abierta más simple: cualquiera puede ver, usar y modificar el código mientras mantenga el aviso de autoría.
