FROM  node:14.16.1-buster

WORKDIR /scripts

COPY src .

RUN npm install

EXPOSE 7001

CMD ["npm", "run"]
