(*
    GeneticFramework

    Copyright 2016 Ivan Sarno

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*)

///Functions that returns a sequence of couples of elements 
///selected from the population. The number of couples that are returned is unknown
module GeneticFramework.Generic.Selectors

let private rand = System.Random()

let distFilter selector population =
    Seq.distinct (selector population)

let singleFilter selector population =
    Seq.filter (fun (x,y) -> x <> y) (selector population)

///Returns random selected couples
let randSel population = 
    let size = Array.length population
    seq {for i in 0..size-1 -> (fst(population.[rand.Next(size)]), fst(population.[rand.Next(size)]))}


///Returns couples composed by elements in opposite positions in the sorted population
let revSel population =
    Array.sortInPlaceBy (fun x -> -snd x) population 
    let length = Array.length population - 1
    let first = seq { for i in 0..length -> fst(population.[i]) }
    let second = seq { for i in length.. -1..0 -> fst(population.[i]) }
    Seq.zip first second 

///Returns couples composed of contiguous elements in the sorted population
let contSel population = 
    Array.sortInPlaceBy (fun x -> -snd x) population
    let size = (Array.length population) - 1
    seq { for i in 1..size -> (fst(population.[i-1]), fst(population.[i])) }


///Returns couples composed of contiguous distinct elements in the sorted population
let distContSel population = 
    Array.sortInPlaceBy (fun x -> -snd x) population
    let size = (Array.length population) - 1
    seq { for i in 1..2..size -> (fst(population.[i-1]), fst(population.[i])) }

///Returns all combination of elements of the population
let allSelector population = 
    seq {for p1 in population do for p2 in population -> (p1,p2)}

let studAllSelector population =
    let best = fst (Array.maxBy snd population)
    seq {for i in population -> (best, fst i)}

let studBestSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    let best = fst (Array.maxBy snd population)
    Seq.sortByDescending snd population |> Seq.tail |> Seq.take length |> Seq.map (fun x-> (best, x))

let studCustomSelector selection ratio population =
    let length = int(double(Array.length population) * ratio) - 1
    let best = fst (Array.maxBy snd population)
    seq {for i in 0..length -> (best, selection population)}

let multiStudAllSelector grade population =
    let bests = Seq.sortByDescending snd population |> Seq.take grade |> Seq.map fst
    seq {for b in bests do for i in population -> (b, fst i)}

let multiStudBestSelector grade ratio population =
    let length = int(double(Array.length population) * ratio)
    let pop = Seq.sortByDescending snd population |> Seq.map fst |> Seq.cache
    let pop2 = Seq.take length pop
    let bests = Seq.take grade pop
    seq {for b in bests do for i in pop2 -> (b, i)}   


let multiStudCustomSelector selection grade ratio population =
    let length = int(double(Array.length population) * ratio) - 1
    let bests = Seq.sortByDescending snd population |> Seq.take grade |> Seq.map fst
    seq {for b in bests do for i in 0..length -> (b, selection population)}

let customSelector selection ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (selection population, selection population)}

let doubleCustomSelector selection1 selection2 ratio population =
    let length = int(double(Array.length population) * ratio) - 1
    seq {for i in 0..length -> (selection1 population, selection2 population)}
    

module Selection =
        let rouletteWheel
	    let stochasticSampling
	    let overSelection
	    let sigmaScaling 
	    let rankBasedSelection
	    let tournamentSelelction
        let linearRanking 

let rouletteWheelSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.rouletteWheel population, Selection.rouletteWheel population)}

let stochasticSamplingSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.stochasticSampling population, Selection.stochasticSampling population)}

let overSelectionSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.overSelection population, Selection.overSelection population)}

let sigmaScalingSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.sigmaScaling  population, Selection.sigmaScaling population)}

let rankBasedSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.rankBasedSelection population, Selection.rankBasedSelection population)}


let tournamentSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.tournamentSelelction population, Selection.rtournamentSelelction population)}

let linearRankingSelector ratio population =
    let length = int(double(Array.length population) * ratio)
    seq {for i in 0..length -> (Selection.linearRanking population, Selection.linearRanking population)}





(*let rankSel initProb population =
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
            List.toArray temp*)

        


