import express from 'express'
import * as fs from 'fs';
import { Result, ResultPart2 } from './02_index.js';

const app = express()
const port = 3000

app.get('/', (req, res) => {
    const puzzle = fs.readFileSync('D://Advent-of-code/TypeScript/input.txt', 'utf-8')
    const elapsedTimes: number[] = []
    const NumberOfRetry: number = 50
    let result: any
    let i = 0
    while(i < NumberOfRetry) {
        const start = performance.now()
        result = Result(puzzle)
        elapsedTimes.push(performance.now() - start)
        i++
    }
    res.send(`${result}\n elapsed Time : ${elapsedTimes.reduce<number>((number, currentValue) => ((number + currentValue) / 2), elapsedTimes[0])}ms`)
})

app.listen(port, () => {
    return console.log(`Express is listening at http://localhost:${port}`)
})
