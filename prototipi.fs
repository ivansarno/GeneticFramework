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

