# Consulta de artículos

Aplicación web desarrollada con:

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Docker Compose

La aplicación permite consultar artículos mediante la ejecución del
procedimiento almacenado `SP_ConsultarArticulo`.

## Requisitos

- Git
- Docker Desktop

No es necesario instalar .NET ni SQL Server localmente.

## Ejecutar el proyecto

Clonar el repositorio:

```bash
git clone URL_DEL_REPOSITORIO
cd pruebaTecnica

docker compose up --build

http://localhost:8080