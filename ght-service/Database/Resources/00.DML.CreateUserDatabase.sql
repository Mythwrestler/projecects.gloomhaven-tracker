CREATE DATABASE "ght-db-nprd"
    WITH
    OWNER = "svc_ght_nprd"
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

GRANT ALL ON DATABASE "ght-db-nprd" TO "svc_ght_nprd";

GRANT TEMPORARY, CONNECT ON DATABASE "ght-db-nprd" TO "svc_ght_nprd";
