import express from 'express';
import ScoreController from './controllers/scoreController';
const routes = express.Router();

const scoreController = new ScoreController();

routes.get('/highscore', scoreController.highscore);
routes.get('/highscore/:nome', scoreController.highscorePorPessoa);
routes.post('/novoHighscore/', scoreController.adicionarScore);

export default routes;