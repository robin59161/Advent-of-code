export const Result = (puzzle) => {
    let calibration = 0;
    for (const puzzleLine of puzzle.split('\n')) {
        calibration += getCalibration(puzzleLine);
    }
    return calibration;
};
export const ResultPart2 = (puzzle) => {
    let calibration = 0;
    for (const puzzleLine of puzzle.split('\n')) {
        calibration += getCalibrationPart2(puzzleLine);
    }
    return calibration;
};
const STRING_NUMBER = {
    "one": 1,
    "two": 2,
    "three": 3,
    "four": 4,
    "five": 5,
    "six": 6,
    "seven": 7,
    "eight": 8,
    "nine": 9
};
const KEYS = Object.keys(STRING_NUMBER);
const getCalibration = (puzzleLine) => {
    let firstNumber = NaN;
    let lastNumber = NaN;
    for (const puzzleItem of puzzleLine) {
        let number = NaN;
        number = parseInt(puzzleItem);
        if (Number.isNaN(number))
            continue;
        else if (Number.isNaN(firstNumber))
            firstNumber = number;
        else
            lastNumber = number;
    }
    return parseInt(`${firstNumber}${Number.isNaN(lastNumber) ? firstNumber : lastNumber}`);
};
const getCalibrationPart2 = (puzzleLine) => {
    let firstNumber = NaN;
    let lastNumber = NaN;
    let lastChar = "";
    for (const puzzleItem of puzzleLine) {
        lastChar += puzzleItem;
        let number = NaN;
        number = parseInt(puzzleItem);
        if (Number.isNaN(number)) {
            const keyInLastChar = KEYS.find((key) => lastChar.endsWith(key));
            if (keyInLastChar) {
                number = STRING_NUMBER[keyInLastChar];
                lastChar = "";
            }
        }
        if (Number.isNaN(number))
            continue;
        else {
            lastChar = "";
            if (Number.isNaN(firstNumber))
                firstNumber = number;
            else
                lastNumber = number;
        }
    }
    return parseInt(`${firstNumber}${Number.isNaN(lastNumber) ? firstNumber : lastNumber}`);
};
