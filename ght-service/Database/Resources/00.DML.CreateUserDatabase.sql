CREATE DATABASE "ght-db-nprd"
    WITH
    OWNER = "ght-db-test"
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.UTF-8'
    LC_CTYPE = 'en_US.UTF-8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

GRANT ALL ON DATABASE "ght-db-nprd" TO "ght-db-test";

GRANT TEMPORARY, CONNECT ON DATABASE "ght-db-nprd" TO "ght-db-test";
