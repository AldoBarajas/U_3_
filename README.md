# Unidad 3 — Estructuras lineales

**Instituto Tecnológico Superior de Monclova** · **Estructuras de Datos**  
**Alumno:** Aldo Alberto Barajas Ancira · **Docente:** Rubén Riojas  
**Tecnología:** C# · .NET 8 · aplicación de consola · Visual Studio 2022 con soporte .NET 8.

## Contenido por tarea

| Proyecto | Estructuras / funcionalidad | Entregables |
| --- | --- | --- |
| 3.1 `Tarea31_Estaticas` | Pila `Push`, `Pop`, `Peek`, `isEmpty`, `isFull`; cola simple; cola circular. Solo `T[]` e índices `top`, `front`, `rear`, `count`. | Código comentado, pruebas, guía PDF y diagrama editable. |
| 3.2 `Tarea32_Dinamicas` | Lista simplemente enlazada `SinglyLinkedList<T>`, `LinkedStack<T>`, `LinkedQueue<T>` y ejemplo separado con `List<T>`, `Stack<T>`, `Queue<T>` de .NET. | Código, comparativa PDF y diagrama de nodos. |
| 3.3 `Tarea33_Restaurante` | Simulación de sala de espera: reservaciones prioritarias y FIFO dentro del mismo tipo. Lista enlazada ordenada desarrollada desde cero. | Proyecto de consola, reporte PDF, UML editable, simulación y casos de prueba. |
| `Pruebas` | **39 pruebas** sin paquetes externos, con retorno 0 cuando todas pasan. | Evidencia reproducible y casos límite. |

La solución `codigo/EstructurasLineales.sln` agrupa los cinco proyectos. El código es independiente del proyecto integrador de ferretería.

## Requisitos

- Windows, Linux o macOS con **SDK .NET 8** instalado; alternativamente, Visual Studio 2022 con desarrollo de escritorio .NET y soporte para .NET 8.
- No se requieren bases de datos, paquetes de NuGet ni conexión a Internet para ejecutar el código.
- Los PDF y diagramas pueden consultarse independientemente del programa.

## Cómo ejecutar (PowerShell / Terminal)

Desde la carpeta donde está este `README.md`:

```powershell
# Compilar toda la solución
 dotnet build codigo/EstructurasLineales.sln

# Pruebas automáticas (se espera 39/39 si compila y pasa)
 dotnet run --project codigo/Pruebas

# Tarea 3.1: demostración y menú interactivo
 dotnet run --project codigo/Tarea31_Estaticas -- --demo
 dotnet run --project codigo/Tarea31_Estaticas

# Tarea 3.2: demostración de lista/pila/cola
 dotnet run --project codigo/Tarea32_Dinamicas

# Tarea 3.3: simulación y modo interactivo
 dotnet run --project codigo/Tarea33_Restaurante -- --demo
 dotnet run --project codigo/Tarea33_Restaurante
```

**En Visual Studio:** abrir `codigo/EstructurasLineales.sln`, compilar con `Ctrl+Shift+B`, elegir el proyecto de inicio (clic derecho → Establecer como proyecto de inicio) y presionar `Ctrl+F5`. Repetir para los cuatro proyectos ejecutables.

### Resultados de referencia (Tarea 3.3)

Para `--demo` se registran: Ana (sin reserva), Beto (reserva), Carla (sin reserva) y Diego (reserva). El orden de atención esperado es **Beto → Diego → Ana → Carla**. Es una **salida esperada**, no una transcripción de una ejecución ya realizada.

### ¿Qué significa cola simple "llena" aun con una celda libre?

La implementación lineal mantiene `rear` avanzando únicamente hacia el final del arreglo. Al llegar al último índice, **no recicla las celdas** liberadas por `Dequeue()` hasta vaciarse por completo. Esto es una decisión didáctica para compararla con la cola circular, que **sí** usa `rear = (rear + 1) % Capacity`.

### ¿Dónde se usan colecciones genéricas nativas?

En las clases estáticas (`StaticStack<T>`, `LinearQueue<T>`, `CircularQueue<T>`) **no se utilizan** `List<T>`, `Queue<T>` ni `Stack<T>`. En la Tarea 3.2 se usan las tres colecciones genéricas predefinidas **solo para ilustrar diferencias**. Estas colecciones tienen crecimiento dinámico de capacidad, pero no implican nodos enlazados internamente.

## Archivos

```
Unidad_3_Estructuras_Lineales/
├─ README.md
├─ codigo/
│  ├─ EstructurasLineales.sln
│  ├─ EstructurasLineales.Core/   # 7 clases de estructura/modelo
│  ├─ Tarea31_Estaticas/         # Menús y ejemplo de índices
│  ├─ Tarea32_Dinamicas/         # Listas, pilas y colas dinámicas
│  ├─ Tarea33_Restaurante/       # Programa de turnos prioritarios
│  └─ Pruebas/                  # 39 pruebas independientes
├─ documentos/
│  ├─ Tarea_3_1_Guia_Implementacion_Estatica.pdf
│  ├─ Tarea_3_2_Comparativo_Estatico_Dinamico.pdf
│  └─ Tarea_3_3_Reporte_Caso_Restaurante.pdf
├─ diagramas/                  # .drawio, .svg, .pdf, .png, .dot
├─ evidencias/                 # Capturas auténticas por completar
└─ guias/
   ├─ GUIA_EVIDENCIAS_Y_GITHUB.md
   ├─ GUION_VIDEO_3_1.md
   ├─ GUION_VIDEO_3_2.md
   └─ GUION_VIDEO_3_3.md
```

## Estado de validación y limitaciones

- Se comprobaron la organización de archivos, la sintaxis XML de proyectos y diagramas, y la presencia de pruebas y funciones requeridas.
- **Este entorno no tiene instalado el SDK de .NET ni compilador C#.** Por ese motivo, el código C# y las 39 pruebas **todavía necesitan compilarse y ejecutarse** en Visual Studio o con `dotnet`. No se presentan capturas simuladas como si fueran resultados de ejecución.
- Los PDF contienen casos de prueba esperados y explicaciones formales. Las evidencias reales deberán capturarse al ejecutar el programa.
- El repositorio público de GitHub y los tres videos de YouTube deben publicarse desde las cuentas del alumno. No se incluyen enlaces inventados.

## GitHub y videos

Ver `guias/GUIA_EVIDENCIAS_Y_GITHUB.md` para comandos de publicación, nombres de capturas y lista de verificación de Teams. Todos los videos deben publicarse en **el mismo canal** y mostrar captura de pantalla con webcam visible **en la esquina inferior derecha**.
