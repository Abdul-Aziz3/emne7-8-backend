FROM mysql:8.0.33

ENV MYSQL_ROOT_PASSWORD=gokstad

# copy from to
COPY studentbloggapi.sql /docker-entrypoint-initdb.d/
COPY user_privileges.sql /docker-entrypoint-initdb.d/

EXPOSE 3306