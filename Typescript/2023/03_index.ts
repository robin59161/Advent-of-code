export const Result = (puzzle: string) => {
    let sum: number = 0
    const puzzleSplit = puzzle.split('\n')
    for(const [indexX, puzzleLine] of puzzleSplit.entries()){
        sum += GetPartPossible(puzzleLine, puzzleSplit, indexX)
    }
    return sum
}

export const ResultPart2 = (puzzle: string) => {
    let sum: number = 0
    for(const puzzleLine of puzzle.split('\n')){
        
    }
    return sum
}

interface NumberValidate {
    number: number
    valid: boolean
}

const GetPartPossible = (puzzleLine: string, puzzleSplit: string[], indexX: number): number => {
    let numbers: NumberValidate[] = []

    let currentNumber: string = ""
    let validNumber: boolean = false
    for(const [indexY, puzzleItem] of puzzleLine.split("").entries()) {
        if(puzzleItem === ".") {
            numbers.push({ number: parseInt(currentNumber), valid: validNumber })
            currentNumber = ""
            validNumber = false
            continue
        }
        if (Number.isNaN(parseInt(puzzleItem))) {
            numbers.push({ number: parseInt(currentNumber), valid: true })
            currentNumber = ""
            validNumber = false
        } else {
            currentNumber += puzzleItem
            if (!validNumber) {
                let i = -1
                while(i <= 1 && !validNumber) {
                    let j = -1
                    while (j <= 1 && !validNumber) {
                        if((indexX + i) >= 0 && (indexX + i) < puzzleSplit.length) {
                            let line = puzzleSplit[indexX + i]
                            if((indexY + j) >= 0 && (indexY + j) < line.length) {
                                let annexe = line[indexY + j]

                                if(Number.isNaN(parseInt(annexe)) && annexe !== "."){
                                    validNumber = true
                                }
                            }
                        }
                        j++
                    }
                    i++
                }
            }
        }
    }

    numbers.push({ number: parseInt(currentNumber), valid: validNumber })

    return numbers.filter(a => a.valid).reduce<number>((a, b) => a + (Number.isNaN(b.number) ? 0 : b.number), 0)
}