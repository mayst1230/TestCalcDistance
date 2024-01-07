FROM node:18.0.0-alpine
WORKDIR /app
COPY . .
RUN yarn add react-scripts
RUN yarn build
CMD ["yarn", "start"]