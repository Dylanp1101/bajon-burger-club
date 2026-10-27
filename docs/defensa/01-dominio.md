# Fase 1: dominio

## Qué hay en `BajonBurgerClub.LogicaNegocio`

```
Entidades/              Hamburgueseria, Resena
ValueObjects/           Puntaje, Ubicacion
Excepciones/            DominioException y una por concepto
InterfacesEntidades/    IValidable
InterfacesRepositorios/ IRepositorio<T>, IRepositorioHamburgueseria
```

Una `Hamburgueseria` tiene una `Ubicacion` y, si ya la probé, una `Resena`. La reseña tiene 5 `Puntaje` (sabor, carne, precio/calidad, pan, papas) y calcula su `NotaFinal`.

## Reglas de negocio y dónde viven

| Regla | Dónde |
|---|---|
| Un puntaje va de 1 a 10 | constructor de `Puntaje` |
| La ubicación está en uno de los 19 departamentos y dentro de Uruguay | constructor de `Ubicacion` |
| La nota final pondera: sabor 30%, carne 25%, precio/calidad 20%, pan 15%, papas 10% | `Resena.NotaFinal` |
| El comentario corto es obligatorio y tiene hasta 140 caracteres | `Resena.Validar()` |
| No se reseña con fecha futura y el precio es mayor a 0 | `Resena.Validar()` |
| Un local sin reseña está "sin probar" | `Hamburgueseria.EstaProbada` |
| La reseña solo se pone con `Calificar()` | `private set` en `Hamburgueseria.Resena` |

## Preguntas de defensa

**¿Qué diferencia hay entre una entidad y un value object?**
La entidad tiene identidad (un `Id`): dos hamburgueserías con el mismo nombre siguen siendo dos locales distintos. El value object se define solo por su valor: un `Puntaje` de 8 es igual a cualquier otro de 8. Por eso `Puntaje` y `Ubicacion` son `record`, que compara por valor, y además son inmutables (solo tienen `get`).

**¿Por qué `Puntaje` y no un `int`?**
Porque con un `int` cualquiera podría poner 11 o -5. Con `Puntaje`, la regla de 1 a 10 está en un solo lugar y es imposible crear un puntaje inválido.

**¿Por qué las propiedades tienen `private set`?**
Encapsulamiento: desde afuera se leen pero no se cambian. Así el objeto no puede quedar en un estado inválido después de creado, y los cambios pasan por métodos con nombre, como `Calificar()`.

**¿Por qué `NotaFinal` no se guarda en la base?**
Porque se deriva de los criterios. Si la guardara, podría quedar desincronizada con los puntajes, y si cambio los pesos tendría que recalcular toda la tabla. Al calcularla siempre está bien.
Consecuencia: la base no puede ordenar por `NotaFinal` (no es una columna), así que el ranking se ordena en memoria en el caso de uso. Con pocas decenas de locales no es un problema.

**¿Por qué `decimal` y no `double` para la nota y el precio?**
`double` guarda los números en binario y tiene errores de redondeo (`0.1 + 0.2 = 0.30000000000000004`). `decimal` es exacto en base 10, que es lo correcto para notas y dinero.

**¿Para qué es el constructor privado sin parámetros?**
EF Core lo usa para crear el objeto al leerlo de la base y después completar las propiedades. Es privado para que desde el código nadie pueda crear una entidad vacía salteándose la validación.

**¿Por qué las interfaces de repositorio están en el dominio y no en AccesoDatos?**
Inversión de dependencias: el dominio dice *qué* necesita ("dame las hamburgueserías probadas") y AccesoDatos decide *cómo* (EF Core, SQL Server). Si cambio la base, el dominio y los casos de uso no se tocan.

**¿Por qué `IRepositorio<T>` es genérico?**
Las operaciones básicas (Add, Update, Remove, FindById, FindAll) son iguales para cualquier entidad. `IRepositorioHamburgueseria` hereda esas y agrega las propias del negocio.

**¿Cómo testeaste el dominio?**
Con xUnit, sin base de datos ni web, justamente porque el dominio no depende de nadie. `[Fact]` es un caso, y `[Theory]` + `[InlineData]` corre el mismo test con varios valores.
