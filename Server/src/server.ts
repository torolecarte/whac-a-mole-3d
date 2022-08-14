const http = require('http');
import express from 'express';
import cors from 'cors';
import routes from './routes';

const app = express();
const dotenv = require('dotenv/config');
const cookieParser = require('cookie-parser');
const bodyParser = require('body-parser');

app.use(cors());
app.use(express.json());
app.use(routes);
routes.use(bodyParser.urlencoded({ extended: true }));
routes.use(bodyParser.json());
routes.use(cookieParser());
  
const server = http.createServer((req: Request, res: Response) => 
{
    res.status;
});

app.listen(process.env.PORT || '3000', () => 
{
    console.log(`Server running`);
});