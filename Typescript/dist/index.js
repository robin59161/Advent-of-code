import express from 'express';
import * as fs from 'fs';
import { ResultPart2 } from './01/01_index.js';
const app = express();
const port = 3000;
app.get('/', (req, res) => {
    const puzzle = fs.readFileSync('D://Advent-of-code/TypeScript/input.txt', 'utf-8');
    const start = performance.now();
    const result = ResultPart2(puzzle);
    const elapsed = performance.now() - start;
    res.send(`${result}\n elapsed Time : ${elapsed}ms`);
});
app.listen(port, () => {
    return console.log(`Express is listening at http://localhost:${port}`);
});
