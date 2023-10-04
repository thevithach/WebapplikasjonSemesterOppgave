# WebapplikasjonSemesterOppgave

Kjør i terminal: 
docker pull mariadb

docker run --rm --name is201-mariadb -p 127.0.0.1:3306:3306/tcp -v /my/own/datadir:/var/lib/mysql -e
MYSQL_ROOT_PASSWORD=Testingtesting1234 -d mariadb:latest

/my/own/datadir mappen kan endres til din egen, ellers lag mappen my, own og datadir.

Gå i terminalen og gå deretter i my/own/datadir mappen også skriv pwd for å få pathen også erstatt /my/own/datadir med det du får fra pwd. Eks: /users/my/own/datadir

Etter du har satt opp database:

Kjør i terminal i prosjektmappe:
dotnet ef database update --project ./WebapplikasjonSemesterOppgave
