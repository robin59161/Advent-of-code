export const Result = (puzzle: string) => {
    let ids: number = 0
    for(const puzzleLine of puzzle.split('\n')){
        let puzzleSplit = puzzleLine.split(":")
        if(PossibleGame(puzzleSplit[1]))
            ids += GetGameId(puzzleSplit[0])
    }
    return ids
}

const RED_CUBES = 12
const GREEN_CUBES = 13
const BLUE_CUBES = 14

export const ResultPart2 = (puzzle: string) => {
    let ids: number = 0
    for(const puzzleLine of puzzle.split('\n')){
        let puzzleSplit = puzzleLine.split(":")
        ids += GetPowerFewest(puzzleSplit[1])
    }
    return ids
}

const PossibleGame = (puzzleLine: string): boolean => {
    let red: number = 0
    let blue: number = 0
    let green: number = 0

    for(const set of puzzleLine.split(";")){
        const setColors = set.split(",")
        for(const setColor of setColors) {
            const [_, numberOf, color] = setColor.split(" ")
            const numberOfColor = parseInt(numberOf)
            if(color === "blue" && numberOfColor > blue)
                blue = numberOfColor
            else if (color === "red" && numberOfColor > red)
                red = numberOfColor
            else if(color === "green" && numberOfColor > green)
                green = numberOfColor
        }
    }

    return (red <= RED_CUBES && blue <= BLUE_CUBES && green <= GREEN_CUBES)
}

const GetGameId = (puzzleLine: string): number => (parseInt(puzzleLine.replace("Game", "")))

const GetPowerFewest = (puzzleLine: string): number => {
    let red: number = 0
    let blue: number = 0
    let green: number = 0

    for(const set of puzzleLine.split(";")){
        const setColors = set.split(",")
        for(const setColor of setColors) {
            const [_, numberOf, color] = setColor.split(" ")
            const numberOfColor = parseInt(numberOf)
            if(color === "blue" && numberOfColor > blue)
                blue = numberOfColor
            else if (color === "red" && numberOfColor > red)
                red = numberOfColor
            else if(color === "green" && numberOfColor > green)
                green = numberOfColor
        }
    }

    return (red * blue * green)
}