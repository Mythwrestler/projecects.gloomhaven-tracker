FROM registry.casperinc.net/node:14 as builder

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
# Copy Over Types
COPY ./types ./types
# Copy Over .env files
COPY ./.env.* ./
# Copy Over Source Code
COPY ./src ./src

ARG BUILD_ENV=local

RUN echo "Build Env is $BUILD_ENV"
RUN if [ "$BUILD_ENV" = "local" ] ; then npm run build:local ; fi ;
RUN if [ "$BUILD_ENV" = "nprd" ] ; then npm run build:nprd ; fi ;
RUN if [ "$BUILD_ENV" = "prod" ] ; then npm run build:prod ; fi ;

FROM registry.casperinc.net/nginx:alpine

WORKDIR /usr/share/nginx/html

RUN rm -rf ./*

COPY --from=builder /app/public .

ENTRYPOINT ["nginx", "-g", "daemon off;"]