let private rand = System.Random();;

let rankSel initProb population =
    Array.sortInPlaceBy (fun x -> -snd x) population 
    let mutable temp = []
    for i in 0..(Array.length population - 1) do
        let prob = initProb * ((1.0 - initProb) ** double(i))
        let choice = rand.NextDouble()
        temp <- if prob >= choice then population.[i]::temp else temp
    List.toArray temp



let tournamentSel prob selectRatio population = 
    let length = Array.length population
    let selections = int(float(length) * selectRatio)
    let f (first, second) = 
        let choice = rand.NextDouble()
        match (first, second, choice) with
        (first, second, choice) when snd(first) > snd(second) && choice > prob -> first
        |(first, second, choice) when snd(first) > snd(second) && choice <= prob -> second
        |(first, second, choice) when snd(first) <= snd(second) && choice > prob -> second
        |(first, second, choice) when snd(first) <= snd(second) && choice <= prob -> first
    [|for i in 1..selections -> f(population.[rand.Next(length)], population.[rand.Next(length)])|]

let rouletteSel population =
    let sum = double(Array.sumBy (fun x -> snd x) population)
    let mutable temp = []
    for i in 0..(Array.length population - 1) do
        let prob = double(snd(population.[i]))/sum
        let choice = rand.NextDouble()
        temp <- if prob >= choice then population.[i]::temp else temp
    List.toArray temp

//____________________EVOLUTION_______________________________________________

let private proto2 reproduction split stop population =
    let rec routine population =
        let toNext, toRep: 'a seq * 'a seq = split population
        let generation = Array.ofSeq (Seq.append toNext (reproduction toRep))
        if stop generation then generation else routine generation
    routine population

let private protoFM population generation size =
    let _,min = Array.last population 
    let bests = Seq.filter (fun (_,y) -> y > min) generation
    sortMerge population bests size

//_____________________CROSS__________________________________________________
///Single point cross on elements of different size, 
///distributor fun selects a random pivot
let difSingleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let pivot = distributor size
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son2, son1);;
    

    
///Swap the elements at 2 different random index
///swaps is the number of the swaps  
let private doubleSwapCross distributor1 distributor2 swaps length (parent1, parent2) =
    let son1 = Array.copy parent1
    let son2 = Array.copy parent1
    for _ in 0..swaps do
        let index1 = distributor1 length
        let index2 = distributor2 length
        let temp = son1.[index1]
        son1.[index1] <- son2.[index2]
        son2.[index2] <- temp
    (son1, son2)
///Like doubleSwapCross but number of swaps is random between 1 and maxSwaps.
let private dynamicDoubleSwapCross distributor1 distributor2 maxSwaps length (parent1, parent2) = 
    let swaps = rand.Next(1, maxSwaps)
    doubleSwapCross distributor1 distributor2 swaps length  (parent1, parent2)


///Double point cross on elements of different size, 
///distributor fun selects a random pivot
let difDoubleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let x = distributor size
    let mutable y = distributor size
    while x=y do
        y <- distributor size
    let pivot1 = min x y
    let pivot2 = max x y
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1)
    


///Take a basic crosser and select dynamically the length of the shortest parent.
let diffMetaCross basicCross (parent1, parent2) =
    let length = min (Array.length parent1) (Array.length parent1)
    basicCross length (parent1, parent2)



///Take a basic crosser and select dynamically the length of the parents.
let dynamicLengthMetaCross basicCross (parent1, parent2) =
     let length = Array.length parent1
     basicCross length (parent1, parent2)

