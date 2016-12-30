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


///Generic Evolution algorithms
namespace GeneticFramework.Generic
    module NewSelection =

        let private rand = System.Random()

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

    module Coupling =

        let customCoupler s1 s2 ratio population =
            let size = Array.length population / ratio 
            [|for i in 1..size -> (s1 population, s2 population)|]

        let studCoupler s ratio population = 
            let size = Array.length population / ratio 
            let max = Array.maxBy snd 
            [|for i in 1..size -> (max, s population)|]

    