ARG REGISTRY
FROM ${REGISTRY}node:14 as builder

WORKDIR /app

# Install Node Modules
COPY ./.npmrc ./
COPY ./package.json ./
RUN npm install

# Copy Over Project Build Configurations
COPY ./rollup.config.js ./
COPY ./.eslintrc.js ./
COPY ./tsconfig.json ./
COPY ./postcss.config.js ./
COPY ./tailwind.config.js ./

# Copy Over Static Content
COPY ./public ./public
RUN mkdir ./public/build
# Copy Over Types
COPY ./types ./types
# Copy Over .env files
COPY ./.env.* ./
# Copy Over Source Code
COPY ./src ./src

RUN npm run prepare

ARG BUILD_ENV=local

RUN echo "Build Env is $BUILD_ENV"
RUN if [ "$BUILD_ENV" = "local" ] ; then npm run build:local ; fi ;
RUN if [ "$BUILD_ENV" = "nprd" ] ; then npm run build:nprd ; fi ;
RUN if [ "$BUILD_ENV" = "prod" ] ; then npm run build:prod ; fi ;

ARG REGISTRY
FROM ${REGISTRY}nginx:alpine

WORKDIR /usr/share/nginx/html

RUN rm -rf ./*

COPY --from=builder /app/public .
COPY nginx.conf /etc/nginx/nginx.conf

ENTRYPOINT ["nginx", "-g", "daemon off;"]
