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
    let i = 0;
    while ((Number.isNaN(firstNumber) || Number.isNaN(lastNumber)) && i < puzzleLine.length) {
        let number = NaN;
        number = parseInt(puzzleLine[i]);
        if (Number.isNaN(firstNumber) && !Number.isNaN(number))
            firstNumber = number;
        number = parseInt(puzzleLine[puzzleLine.length - (i + 1)]);
        if (Number.isNaN(lastNumber) && !Number.isNaN(number))
            lastNumber = number;
        i++;
    }
    return parseInt(`${firstNumber}${lastNumber}`);
};
const getCalibrationPart2 = (puzzleLine) => {
    let firstNumber = NaN;
    let lastNumber = NaN;
    let lastChar = ["", ""];
    let i = 0;
    while ((Number.isNaN(firstNumber) || Number.isNaN(lastNumber)) && i < puzzleLine.length) {
        lastChar[0] = lastChar[0] + puzzleLine[i];
        lastChar[1] = puzzleLine[puzzleLine.length - (i + 1)] + lastChar[1];
        let number = NaN;
        number = parseInt(puzzleLine[i]);
        if (Number.isNaN(firstNumber) && !Number.isNaN(number))
            firstNumber = number;
        else if (Number.isNaN(firstNumber)) {
            const keyInLastChar = KEYS.find((key) => lastChar[0].endsWith(key));
            if (keyInLastChar) {
                firstNumber = STRING_NUMBER[keyInLastChar];
                lastChar[0] = "";
            }
        }
        number = parseInt(puzzleLine[puzzleLine.length - (i + 1)]);
        if (Number.isNaN(lastNumber) && !Number.isNaN(number))
            lastNumber = number;
        else if (Number.isNaN(lastNumber)) {
            const keyInLastChar = KEYS.find((key) => lastChar[1].startsWith(key));
            if (keyInLastChar) {
                lastNumber = STRING_NUMBER[keyInLastChar];
                lastChar[1] = "";
            }
        }
        i++;
    }
    return parseInt(`${firstNumber}${lastNumber}`);
};
