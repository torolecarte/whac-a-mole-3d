## API
API para utilização no highscore, o score é salvo em **memória** se a api resetar os valores irão ser perdidos.

[![Generic badge](https://img.shields.io/badge/NODEJS-GREEN.svg)](https://nodejs.org/en/)

```
npm install
npm start
```

## Endpoint

> GET /highscore

> GET /highscore/{nome}

> POST /novoHighscore
```
{
	"nome":"teste2",
	"ponto":1232
}
```