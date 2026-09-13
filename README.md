# Biblioteca API REST

¡Bienvenido al repositorio de **Biblioteca API REST**! Este proyecto fue desarrollado como parte de una capacitación *Trainee* en **Bitwise SA** durante el año 2024. Es una API RESTful construida con .NET para gestionar los recursos básicos de una biblioteca.

## Tecnologías y Herramientas

El proyecto está desarrollado utilizando el stack moderno de Microsoft para aplicaciones web:

- **.NET 7.0** (C#)
- **ASP.NET Core Web API**
- **Entity Framework Core** (OR/M para acceso a datos)
- **SQL Server** (Base de datos)
- **AutoMapper** (Mapeo de objetos/DTOs)
- **Swagger / OpenAPI** (Documentación y prueba de la API)

## Arquitectura y Patrones de Diseño

El código fuente está estructurado siguiendo buenas prácticas de ingeniería de software para mantenerlo limpio, escalable y mantenible:

- **Patrón Repositorio (`Repository Pattern`)**: Se utiliza `IGenericRepository` para estandarizar las operaciones CRUD básicas y repositorios específicos (como `ILibroRepository`) para consultas más complejas con datos relacionados.
- **DTOs (Data Transfer Objects)**: Se separa el modelo de dominio de los datos que se exponen o reciben en los endpoints, usando AutoMapper para transformarlos fácilmente.
- **Inyección de Dependencias (DI)**: Ampliamente utilizado para inyectar servicios, repositorios y el mapper en los controladores.
- **Entity Framework Migrations**: Control de versiones de la estructura de la base de datos a través de migraciones de EF Core.

## Entidades Principales

La API provee endpoints completos (CRUD) para administrar los siguientes recursos:

- **Libros** (`/api/Libro`)
- **Autores** (`/api/Autor`)
- **Géneros** (`/api/Genero`)
- **Comentarios** (`/api/Comentario`)

*Nota: Algunos endpoints incluyen la obtención de datos relacionados (e.g. Libros con su Autor y Género asociado).*

## Cómo ejecutar el proyecto localmente

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/LuisCarmagnoli/Biblioteca-API-REST.git
   ```
2. **Configurar la base de datos:**
   - Abre el archivo `appsettings.json` o `appsettings.Development.json` y asegúrate de configurar tu cadena de conexión (Connection String) a una instancia de SQL Server local.
3. **Aplicar las migraciones:**
   Abre la Consola del Administrador de Paquetes en Visual Studio (o usa la CLI de .NET) y ejecuta:
   ```bash
   Update-Database
   ```
   *(O alternativamente: `dotnet ef database update`)*
4. **Ejecutar la aplicación:**
   Al correr el proyecto, se abrirá automáticamente **Swagger** en tu navegador, donde podrás probar todos los endpoints de manera interactiva.

---
*Desarrollado por [Luis Carmagnoli](https://github.com/LuisCarmagnoli) - 2024*
