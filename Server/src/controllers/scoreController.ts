import { Request, Response } from 'express';
import Joi, { any } from 'joi';
import Score from '../models/Score';

let scores: Score[] = [];

class ScoreController 
{
    async highscore(request: Request, response: Response) 
    {
        try {
            return response.json(scores.sort((a, b) => (a.ponto < b.ponto ?  1 : -1)));
        } 
        catch (err) 
        {
            console.log(err);
            return response.json({ message: err });
        }
    }

    async highscorePorPessoa(request: Request, response: Response) 
    {
        try {
            const { nome } = request.params;     
            return response.json(scores.find(x => x.nome === nome));
        } 
        catch (err) 
        {
            console.log(err);
            return response.json({ message: err });
        }
    }

    async adicionarScore(request: Request, response: Response) 
    {
        try {

            const schema = {
                nome: Joi.string().min(1),
                ponto: Joi.number()
            };

            const {
                nome,
                ponto
            } = request.body;

            const { error } = Joi.validate(request.body, schema);
            if (error) return response.send(error.details[0].message);

            scores.push(request.body);

            return response.json(scores.sort((a, b) => (a.ponto < b.ponto ?  1 : -1)));
        } 
        catch (err) 
        {
            console.log(err);
            return response.json({ message: err });
        }
    }

}

export default ScoreController;