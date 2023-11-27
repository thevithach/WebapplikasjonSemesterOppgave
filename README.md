# WebapplikasjonSemesterOppgave
NB!: Admin bruker "Admin@gmail.com" "Admin123*"

Kjør i terminal: 
docker pull mariadb

Bash(MAC & LINUX): docker run --rm --name mariadb -p 3308:3306/tcp -v "$(pwd)/database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=12345 -d mariadb:10.5.11	

Powershell(WINDOWS): docker run --rm --name mariadb -p 3308:3306/tcp -v "%cd%\database":/var/lib/mysql -e MYSQL_ROOT_PASSWORD=12345 -d mariadb:10.5.11

Gå til pathen for prosjektmappen.

Windows: Gå i package manager console og skriv "update-database"

Alternativ løsning / (Mac & Linux)

Kjør i terminal i prosjektmappe:

dotnet tool install --global dotnet-ef

dotnet ef database update --project ./WebapplikasjonSemesterOppgave --context DBContextSample

Appsettings.json må endres i Server fra "localhost" til "172.17.0.1" dersom det skal kjøres på docker.

Kjør dockerfilen i mappen for å kjøre på applikasjonen på docker. 

