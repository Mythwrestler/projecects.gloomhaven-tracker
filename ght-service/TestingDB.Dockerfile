ARG REGISTRY
FROM ${REGISTRY}postgres:14-alpine3.14 AS builder

WORKDIR /docker-entrypoint-initdb.d

# Copy in project files
COPY /Database/Resources .