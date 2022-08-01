ARG REGISTRY
FROM ${REGISTRY}postgres:13-alpine3.14 AS builder

WORKDIR /docker-entrypoint-initdb.d

# Copy in project files
COPY /Service/Resources .