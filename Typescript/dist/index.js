import express from 'express';
import * as fs from 'fs';
import { ResultPart2 } from './03_index.js';
const app = express();
const port = 3000;
app.get('/', (req, res) => {
    const puzzle = fs.readFileSync('D://Advent-of-code/TypeScript/input.txt', 'utf-8');
    const elapsedTimes = [];
    const NumberOfRetry = 1;
    let result;
    let i = 0;
    while (i < NumberOfRetry) {
        const start = performance.now();
        result = ResultPart2(puzzle);
        elapsedTimes.push(performance.now() - start);
        i++;
    }
    res.send(`${result}\n elapsed Time : ${elapsedTimes.reduce((number, currentValue) => ((number + currentValue) / 2), elapsedTimes[0])}ms`);
});
app.listen(port, () => {
    return console.log(`Express is listening at http://localhost:${port}`);
});
