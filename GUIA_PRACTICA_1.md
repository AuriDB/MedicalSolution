# Guía de Implementación — Práctica 1

Esta guía te explica paso a paso qué hacer en Visual Studio para que todo funcione.
Todos los archivos `.cs` ya están listos en tu carpeta — solo necesitas instalar los paquetes NuGet y correr el proyecto.

---

## Paso 1 — Instalar los paquetes NuGet (RestSharp y Flurl)

Debes agregar las dos librerías al proyecto **AppLogic**.

### Opción A: con la Consola de NuGet (recomendado)
1. En Visual Studio, ve al menú: **Tools → NuGet Package Manager → Package Manager Console**
2. Asegúrate de que el proyecto seleccionado sea **AppLogic** en el dropdown de la consola.
3. Ejecuta estos dos comandos uno por uno:

```
Install-Package RestSharp
Install-Package Flurl.Http
```

### Opción B: con la interfaz gráfica
1. Click derecho sobre el proyecto **AppLogic** → **Manage NuGet Packages**
2. Busca `RestSharp` → instalar
3. Busca `Flurl.Http` → instalar

---

## Paso 2 — Verificar los archivos modificados/creados

Ya están listos en tu carpeta. Revisalos en Visual Studio:

### Archivos modificados:
| Archivo | Qué cambió |
|---|---|
| `AppLogic/RHConnector.cs` | Se agregaron 6 métodos nuevos a la interface y la clase |
| `API/Controllers/RHController.cs` | Se agregaron todos los endpoints nuevos |
| `API/Program.cs` | Se registraron los 2 servicios nuevos |

### Archivos nuevos creados:
| Archivo | Para qué sirve |
|---|---|
| `AppLogic/RHRestSharpConnector.cs` | Manager que usa RestSharp para llamar la API |
| `AppLogic/RHFlurConnector.cs` | Manager que usa Flurl para llamar la API |

---

## Paso 3 — Compilar y probar

1. Presiona **Ctrl+Shift+B** para compilar (Build Solution).
2. Si hay errores, seguramente es porque falta instalar los paquetes NuGet del Paso 1.
3. Presiona **F5** para correr el proyecto.
4. Se abrirá Swagger en el navegador — verás todos los endpoints nuevos.

---

## Endpoints disponibles después de la práctica

### Métodos originales
- `GET /api/RHConnector/GetAllEmployees` — todos los empleados (HttpClient)
- `GET /api/RHConnector/GetAllSpecialties` — especialidades

### Métodos nuevos (Parte 1)
- `GET /api/RHConnector/GetEmployeeManager/{employeeId}` — manager del empleado
- `GET /api/RHConnector/GetOldestEmployee` — empleado(s) con más años
- `GET /api/RHConnector/GetNewestEmployee` — empleado(s) con menos años
- `GET /api/RHConnector/GetEmployeeById/{id}` — empleado por Id (null si no existe)
- `GET /api/RHConnector/GetEmployeesWithMoreThan/{years}` — empleados con >= N años
- `GET /api/RHConnector/GetEmployeesWithLessThan/{years}` — empleados con <= N años

### Métodos con librerías alternativas (Parte 2)
- `GET /api/RHConnector/GetAllEmployeesRestSharp` — empleados usando RestSharp
- `GET /api/RHConnector/GetAllEmployeesFlur` — empleados usando Flurl

---

## Cómo funciona la arquitectura (resumen)

```
Controller (API)
    └── llama a → IRHConnector       → RHConnector        (usa HttpClient)
    └── llama a → IRHRestSharpConnector → RHRestSharpConnector (usa RestSharp)
    └── llama a → IRHFlurConnector   → RHFlurConnector    (usa Flurl)
```

Cada "Connector" en la capa **AppLogic** hace el llamado HTTP a la API externa
(`https://rh-central.azurewebsites.net`) usando su respectiva librería.
La lógica de filtrado (oldest, newest, years, manager) se hace dentro del `RHConnector`
sobre la lista que ya viene de la API.

---

## Lógica de los métodos nuevos (explicación simple)

- **GetEmployeeManager(id)**: busca el empleado por Id, agarra su campo `ManagerId`, y busca ese otro empleado en la lista.
- **GetOldestEmployee**: parsea todos los `HiringDate` como fechas, encuentra la más antigua, devuelve los que tengan esa fecha.
- **GetNewestEmployee**: igual pero con la fecha más reciente.
- **GetEmployeeById(id)**: busca en la lista por Id, devuelve null si no encuentra.
- **GetEmployeesWithMoreThan(years)**: calcula fecha de corte = hoy - N años. Devuelve empleados cuyo `HiringDate` sea ANTES de esa fecha (llevan más tiempo).
- **GetEmployeesWithLessThan(years)**: igual pero devuelve empleados cuyo `HiringDate` sea DESPUÉS de esa fecha (llevan menos tiempo).

---

## Entregables (recordatorio)

- [ ] Código en archivo .zip con tu nombre y apellidos
- [ ] Archivo .txt con link al repositorio de GitHub (debe ser público)
- [ ] En el mismo .txt, link de la app publicada en Azure App Services
- [ ] Todo en un único .zip

---

## Publicar en Azure (si no lo has hecho)

1. En Visual Studio, click derecho sobre el proyecto **API** → **Publish**
2. Selecciona **Azure** → **Azure App Service (Windows o Linux)**
3. Sigue el asistente para crear o seleccionar tu App Service
4. Click en **Publish**
5. Copia la URL que te da Azure y pégala en tu archivo .txt
