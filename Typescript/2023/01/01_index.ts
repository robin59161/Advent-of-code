export const Result = (puzzle: string) => {
    let calibration: number = 0
    for(const puzzleLine of puzzle.split('\n')){
        calibration += getCalibration(puzzleLine)
    }
    return calibration
}

export const ResultPart2 = (puzzle: string) => {
    let calibration: number = 0
    for(const puzzleLine of puzzle.split('\n')){
        calibration += getCalibrationPart2(puzzleLine)
    }
    return calibration
}

type FirstDigit = "one" | "two" | "three" | "four" | "five" | "six" | "seven" | "eight" | "nine"

const STRING_NUMBER: {
    [K in FirstDigit]: number
} = { 
    "one": 1,
    "two": 2,
    "three": 3,
    "four": 4,
    "five": 5,
    "six": 6,
    "seven": 7,
    "eight": 8,
    "nine": 9
}

const KEYS = Object.keys(STRING_NUMBER)

const getCalibration = (puzzleLine: string) => {
    let firstNumber: number = NaN
    let lastNumber: number = NaN
    for(const puzzleItem of puzzleLine){
        let number: number = NaN
        number = parseInt(puzzleItem)

        if(Number.isNaN(number))
            continue
        else if(Number.isNaN(firstNumber))
            firstNumber = number
        else 
            lastNumber = number
    }
    return parseInt(`${firstNumber}${Number.isNaN(lastNumber) ? firstNumber : lastNumber}`)
}


const getCalibrationPart2 = (puzzleLine: string) => {
    let firstNumber: number = NaN
    let lastNumber: number = NaN
    let lastChar: string = ""
    for(const puzzleItem of puzzleLine){
        lastChar += puzzleItem
        let number: number = NaN
        number = parseInt(puzzleItem)

        if(Number.isNaN(number)){
            const keyInLastChar = KEYS.find((key) => lastChar.endsWith(key))
            if(keyInLastChar) {
                number = STRING_NUMBER[keyInLastChar as FirstDigit]
                lastChar = ""
            }
        }

        if(Number.isNaN(number))
            continue
        else {
            lastChar = ""
            if(Number.isNaN(firstNumber))
                firstNumber = number
            else 
                lastNumber = number
        }
    }
    console.log(`${puzzleLine} -> ${firstNumber}${Number.isNaN(lastNumber) ? firstNumber : lastNumber}`)
    return parseInt(`${firstNumber}${Number.isNaN(lastNumber) ? firstNumber : lastNumber}`)
}