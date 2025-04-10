# Aralytiks - Test - C# Web API Assessment Project

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (for Oracle database setup)
- [Git](https://git-scm.com/) (to clone the repository)

## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/MusaAliji/AralytiksTest.git
cd AralytiksTest
```

### 2. Configure the Oracle Database with Docker Compose

The project includes a docker-compose.yml file to set up an Oracle database. Ensure Docker is running, then:
```bash
docker-compose up -d
```

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Build the Project
```bash
dotnet build
```

### 5. Apply Database Migrations
Ensure the Oracle database is running, then apply the Entity Framework Core migrations:
```bash
cd AralytiksTest2
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6. Run the Application
```bash
cd ..
dotnet run --project AralytiksTest2 --launch-profile https
```
