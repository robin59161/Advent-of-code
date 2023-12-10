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
    const puzzleSplit = puzzle.split('\n')
    for(const [indexX, puzzleLine] of puzzleSplit.entries()){
        sum += GetGear(puzzleLine, puzzleSplit, indexX)
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

interface NumberXY {
    number: number
    x: number
    startY: number
    endY: number
}

const GetGear = (puzzleLine: string, puzzleSplit: string[], indexX: number): number => {
    let sumGear = 0

    for(const [indexY, puzzleItem] of puzzleLine.split("").entries()){
        if(puzzleItem === "*") {
            let product = 0
            const numbers: NumberXY[] =  []
            let i = -1
            while(i <= 1) {
                let j = -1
                while (j <= 1) {
                    if((indexX + i) >= 0 && (indexX + i) < puzzleSplit.length) {
                        let line = puzzleSplit[indexX + i]
                        if((indexY + j) >= 0 && (indexY + j) < line.length) {
                            let annexe = line[indexY + j]
                            if(!Number.isNaN(parseInt(annexe))){
                                if(numbers.find((value)=> value.x === indexX + i && value.startY <= (indexY + j) && value.endY > (indexY + j)) === undefined) {
                                    const number = GetNumber(puzzleSplit[indexX + i], indexY + j)
                                    product = product === 0 ? number.number : product * number.number
                                    numbers.push({ number: number.number, x: indexX + i, startY: number.startY, endY: number.endY })
                                }
                            }
                        }
                    }
                    j++
                }
                i++
            }
            sumGear += numbers.length >= 2 ? product : 0
            console.log({ numbers, product, puzzleLine, sumGear })
        }
    }

    return sumGear
}

const GetNumber = (line: string, y: number): NumberXY => {
    let numberStr: string = line[y]

    let endY: number = y + 1
    let startY: number = y

    let plusOneXNumber: number = parseInt(line[y + 1])
    let minusOneXNumber: number = parseInt(line[y - 1])
    let isNanPlusOne: boolean = Number.isNaN(plusOneXNumber)
    let isNanMinusOne: boolean = Number.isNaN(minusOneXNumber)

    while(!isNanPlusOne || !isNanMinusOne){
        if(!isNanPlusOne) {
            numberStr += plusOneXNumber
            endY++
            plusOneXNumber = parseInt(line[endY])
            isNanPlusOne = Number.isNaN(plusOneXNumber)
        }
        if(!isNanMinusOne) {
            numberStr = minusOneXNumber + numberStr
            startY--
            minusOneXNumber = parseInt(line[startY - 1])
            isNanMinusOne = Number.isNaN(minusOneXNumber)
        }
    }
    
    return { number: parseInt(numberStr), startY: startY, x: 0, endY }
}