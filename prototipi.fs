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

//_____________________Stop_________________________________________________

type IStop =
    abstract member Stop: ('a * int)[] -> bool
    

type CountStop(iterations) =
    let mutable iteration = iterations
    interface IStop with
    
        member this.Stop (population: ('a * int)[]) = 
            iteration <- iteration - 1
            if iteration = 0 then true else false 

type P1(iterations) =
    let mutable attempts = iterations
    let mutable maxVal = 0

    interface IStop with
        member this.Stop population =
            let max = snd (Array.maxBy snd population)
            if maxVal >= max 
                then attempts <- attempts - 1 else
                maxVal <- max
                attempts <- iterations
            if attempts = 0 then true else false


type P2(iterations) =
    let mutable attempts = iterations
    let mutable maxSum = 0

    interface IStop with
        member this.Stop population =
            let sum = Array.sumBy snd population
            if maxSum >= sum
                then attempts <- attempts - 1 else
                maxSum <- sum
                attempts <- iterations
            if attempts = 0 then true else false

type P3(iterations) =
    let mutable attempts = iterations
    let mutable maxMean = 0.0

    interface IStop with
        member this.Stop population =
            let mean = double(Array.sumBy snd population) / double(Array.length population)
            if maxMean >= mean
                then attempts <- attempts - 1 else
                maxMean <- mean
                attempts <- iterations
            if attempts = 0 then true else false


let andStopCombination stops population =
    Array.forall (fun s -> s population)

let orStopCombination stops population =
    Array.exists (fun s -> s population)

//_____________________Replacement_____________________

let pureElitism population generation =
    let size = Array. length population 
    let newPopulation = Array.append population generation
    Array.sortInPlaceBy (fun s -> -snd s) newPopulation
    Array.take size newPopulation

let e1 ratio population generation =
    let length = Array.length population
    let size = int(double(length) / ratio)
    Array.sortInPlaceBy (fun s -> -snd s) population
    Array.sortInPlaceBy (fun s -> -snd s) generation
    let bestPop = Array.take size population
    let bestGen = Array.take (length - size) generation
    let newPopulation = Array.append (Array.take size population) generation
    Array.sortInPlaceBy (fun s -> -snd s) newPopulation
    newPopulation

let e2 ratio population generation =
    let length = Array.length population
    let size = int(double(length) / ratio)
    Array.sortInPlaceBy (fun s -> -snd s) population
    let bestPop = Array.take size population
    let bestGen = Array.take (length - size) generation
    let newPopulation = Array.append (Array.take size population) generation
    Array.sortInPlaceBy (fun s -> -snd s) newPopulation
    newPopulation

//_____________________Coupling_______________________________

let customCoupler s1 s2 ratio population =
    let size = Array.length population / ratio 
    [|for i in 1..size -> (s1 population, s2 population)|]

let studCoupler s ratio population = 
    let size = Array.length population / ratio 
    let max = Array.maxBy snd 
    [|for i in 1..size -> (max, s population)|]

let bestFilter ratio  population =
    let size = Array.length population / ratio 
    Array.sortInPlaceBy (fun x -> -snd x) population 
    Array.take size population

let customFilter ratio s population =
    let size = Array.length population / ratio
    [|for i in 1..size -> s population|]

let fairFilter ratio1 ratio2 s1 s2 population =
    let size1 = Array.length population / ratio1 
    let size2 = Array.length population / ratio2
    let bests = [|for i in 1..size1 -> s1 population|]
    let others = [|for i in 1..size2 -> s2 population|]
    Array.append bests others
    


        
