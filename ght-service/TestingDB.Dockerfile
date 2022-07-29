ARG REGISTRY
FROM ${REGISTRY}postgresql:14 AS builder

WORKDIR /docker-entrypoint-initdb.d

# Copy in project files
COPY /Database/Resources .