export const Result = (puzzle) => {
    let sum = 0;
    const puzzleSplit = puzzle.split('\n');
    for (const puzzleLine of puzzleSplit) {
        sum += CountWining(puzzleLine);
    }
    return sum;
};
export const ResultPart2 = (puzzle) => {
    let puzzleMapped = puzzle.split('\n').reduce((previousValue, currentValue) => {
        return [...previousValue, mapLineToCard(currentValue)];
    }, []);
    return (countCards(puzzleMapped, puzzleMapped));
};
const CountWining = (puzzleLine) => {
    let winingCount = 0;
    const [wining, card] = puzzleLine.split(":")[1].split("|");
    const winingNumbers = wining.trim().split(" ").map((el) => parseInt(el)).filter((el) => !Number.isNaN(el));
    const cardNumbers = card.trim().split(" ").map((el) => parseInt(el)).filter((el) => !Number.isNaN(el));
    for (const cardNumber of cardNumbers) {
        if (winingNumbers.includes(cardNumber)) {
            winingCount = winingCount === 0 ? 1 : winingCount * 2;
        }
    }
    return winingCount;
};
const countCards = (originalPuzzleMapped, puzzleMapped) => {
    let sum = 0;
    for (const card of puzzleMapped) {
        const winingCount = CountWining2(card);
        sum += 1 + countCards(originalPuzzleMapped, originalPuzzleMapped.filter((value) => value.number <= (card.number + winingCount) && value.number > card.number));
    }
    return sum;
};
const mapLineToCard = (puzzleLine) => {
    let card = { number: 0, winingNumbers: [], cardNumbers: [] };
    const splittedLine = puzzleLine.split(":");
    let i = 0;
    while (card.number === 0 || Number.isNaN(card.number)) {
        card.number = parseInt(splittedLine[0].split(" ")[i]);
        i++;
    }
    const [wining, cards] = splittedLine[1].split("|");
    card.winingNumbers = wining.trim().split(" ").map((el) => parseInt(el)).filter((el) => !Number.isNaN(el));
    card.cardNumbers = cards.trim().split(" ").map((el) => parseInt(el)).filter((el) => !Number.isNaN(el));
    return card;
};
const CountWining2 = (card) => {
    let winingCount = 0;
    for (const cardNumber of card.cardNumbers) {
        if (card.winingNumbers.includes(cardNumber)) {
            winingCount++;
        }
    }
    return winingCount;
};
