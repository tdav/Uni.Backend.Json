# Txt

    https://github.com/OData/AspNetCoreOData
    
    https://github.com/serilog/serilog-sinks-elasticsearch


./sync_server --urls=http://0.0.0.0:10000/

dotnet build -r linux-x64 /p:PublishReadyToRun=true

docker build -t documentconversionwebapi .
docker run -d -p 8080:80 documentconversionwebapi


    1-Ишлатганда postgress базаси учириб ташланади
    2-Клиентда хамма справочниклар жунатилади
        Справочникларда  - SELECT * FROM {0}
    3-Сервердан олишда санани олдинга сурилган куймат жунатилади
        30062000121742  =>  30.06.2020 12:17:42
          


    Info
        https://blog.rsuter.com/automatically-migrate-your-entity-framework-core-managed-database-on-asp-net-core-application-start/
        https://postgis.net/docs/manual-dev/postgis_usage.html
        https://www.npgsql.org/efcore/mapping/nts.html
        https://gis.stackexchange.com/questions/11567/spatial-clustering-with-postgis


    CREATE SERVICE
        nano /etc/systemd/system/wa_sync_server.service
        systemctl enable wa_sync_server.service
        systemctl start wa_sync_server


    METRICS
        https://www.app-metrics.io/samples/grafana/



    CLUSTERING POINTS
        https://gis.stackexchange.com/questions/11567/spatial-clustering-with-postgis


    LINQ EXTENSIONS
        PostgisExtensions



    Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiOTk5OzE7MjszOyIsIm5iZiI6MTU4ODM3MTcxOSwiZXhwIjoxNTg4OTc2NTE5LCJpYXQiOjE1ODgzNzE3MTl9.gpPYeliE90m34Xm-XaXkBjBDW3rtu_LwWYJQ1lo7GTc




    dotnet ef dbcontext scaffold "Server=localhost;Database=ef;User=root;Password=123456;TreatTinyAsBoolean=true;" "Pomelo.EntityFrameworkCore.MySql"



    INSTALL POSTGRESS 12
        https://computingforgeeks.com/how-to-install-postgresql-12-on-centos-7/


    INSTALL POSTGIST
        https://people.planetpostgresql.org/devrim/index.php?/archives/102-Installing-PostGIS-3.0-and-PostgreSQL-12-on-CentOS-8.html


        CREATE ROLE davron WITH LOGIN CREATEDB ENCRYPTED PASSWORD 'grand_online_orders';
        psql -c "CREATE ROLE davron WITH LOGIN CREATEDB ENCRYPTED PASSWORD 'grand_online_orders'"
        psql -c "alter user postgres with password 'grand_online_orders'"

        psql -h localhost -U davron -d orders -a -f init_db.sql


        psql -h localhost -U davron postgres

           DROP DATABASE orders;

           CREATE DATABASE apteka_db
                WITH 
                OWNER = davron
                ENCODING = 'UTF8';


            \c orders   --use database
            CREATE EXTENSION IF NOT EXISTS btree_gin WITH SCHEMA public;
            CREATE EXTENSION IF NOT EXISTS btree_gist WITH SCHEMA public;
            CREATE EXTENSION IF NOT EXISTS fuzzystrmatch WITH SCHEMA public;
            CREATE EXTENSION IF NOT EXISTS postgis WITH SCHEMA public;





        BAKCUP DB
            pg_dump -U davron orders > dbname.bak
            pg_dump -h localhost -U davron orders > dbname.bak

        RESTORE DB
            psql -h localhost -U davron orders < dbname.bak
            psql -h 127.0.0.1 -U davron orders < dbname.bak


    FATAL-IDENT-AUTHENTICATION-FAILED-FOR-USER-UNABLE-TO-CONNECT-TO-POSTGRESQL
        https://confluence.atlassian.com/bitbucketserverkb/fatal-ident-authentication-failed-for-user-unable-to-connect-to-postgresql-779171564.html

        nano /var/lib/pgsql/12/data/pg_hba.conf

        /-----------------------------------/       
        # TYPE  DATABASE        USER            ADDRESS                 METHOD
        # IPv4 local connections:
        host    all             all             127.0.0.1/32            trust
        host    all             all             localhost               trust
        host    all             all             ::1/128               	trust

        # Allow replication connections from localhost, by a user with the
        # replication privilege.
        host    replication     all             127.0.0.1/32            md5
        host    replication     all             ::1/128                 md5
        /-----------------------------------/

        sudo systemctl restart postgresql-12


    FIREWALL
        firewall-cmd --zone=public --add-port=25002/tcp --permanent
	    firewall-cmd --reload

        firewall-cmd --list-all   # get open port list


    Host ASP.NET Core on Linux with Nginx
        https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1


    Ports
        Web App        - 80, 443 
        Web Api Server - 25002
        pgAdmin        - 35002
         

    Init Postgres 12
        sudo su - postgres 
        psql -c "alter user postgres with password 'Ghtg.uz2020'" 
        psql -c "CREATE ROLE davron WITH LOGIN CREATEDB ENCRYPTED PASSWORD 'grand_online_orders'"
        sudo systemctl restart postgresql-12


    sudo yum group install "Development Tools"


    /var/lib/pgsql/12/data/pg_hba.conf


    SCREEN 

        screen -S name
        screen -list        
        screen -r name



    JENKINS
        https://medium.com/fusionqa/how-to-run-jenkins-using-the-root-user-in-linux-centos-79d96749ca5a
        docker run --name jenkins-docker -p 8080:8080 -v /var/run/docker.sock:/var/run/docker.sock gustavoapolinario/jenkins-docker



     
     dotnet publish -c Release /p:PublishDir=/var/www/publish_uni
     dotnet ef migrations script --idempotent --output "/var/www/publish_uni/EFSQLScripts/UniPos.MyDbContext.sql" --context UniPos.MyDbContext

    
    file="/etc/systemd/system/wa_unipos.service"
    if [ -f "$file" ]
    then
	    systemctl stop wa_unipos
    else
	    cp waunipos.service /etc/systemd/system/wa_unipos.service
        systemctl enable wa_unipos.service
    fi
    
    systemctl start wa_unipos
    
    

    DOCKER
        https://itnext.io/smaller-docker-images-for-asp-net-core-apps-bee4a8fd1277


