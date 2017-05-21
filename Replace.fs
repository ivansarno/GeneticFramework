(*
    GeneticFramework

    Copyright 2017 Ivan Sarno

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

///Generic utility functions
namespace GeneticFramework.Generic
    module Replace =

        let private rand = System.Random()

        let private chooseRandom array length =
            let total = Array.length array
            let indexs = Seq.initInfinite (fun _ -> rand.Next total)|> Seq.distinct |> Seq.take length
            Seq.map (fun x -> array.[x]) indexs |> Seq.toArray

        let private chooseBest array length = 
            Array.sortInPlaceBy (fun s -> -snd s) array
            Array.take length array

        let pureElitism population generation =
            let size = Array. length population 
            let newPopulation = Array.append population generation
            chooseBest newPopulation size

        let bestBest ratio population generation =
            let length = Array.length population
            let size = int(double(length) / ratio)
            Array.append (chooseBest population size) (chooseBest generation (length - size))


        let roulettePopFirst population generation =
            let max = double (snd (max (Array.maxBy snd population) (Array.maxBy snd generation)))
            let length = Array.length population
            let merge = Seq.append population generation
            let filter (x,y) = double(y)/max > rand.NextDouble()
            Seq.append (Seq.filter filter merge) population |> Seq.take length |> Seq.toArray

        let rouletteGenFirst population generation =
            let max = double (snd (max (Array.maxBy snd population) (Array.maxBy snd generation)))
            let length = Array.length population
            let merge = Seq.append generation population 
            let filter (x,y) = double(y)/max > rand.NextDouble()
            Seq.append (Seq.filter filter merge) generation |> Seq.take length |> Seq.toArray

        let rouletteSumGenFirst population generation =
            let sum = double (Array.sumBy snd population  + Array.sumBy snd generation)
            let length = Array.length population
            let merge = Seq.append generation population 
            let mutable acc = 0.0
            let filter (x,y) = 
                acc <- acc + y
                acc/sum > rand.NextDouble()
            Seq.append (Seq.filter filter merge) generation |> Seq.take length |> Seq.toArray

        let rouletteSumPopFirst population generation =
            let sum = double (Array.sumBy snd population  + Array.sumBy snd generation)
            let length = Array.length population
            let merge = Seq.append population generation 
            let mutable acc = 0.0
            let filter (x,y) = 
                acc <- acc + y
                acc/sum > rand.NextDouble()
            Seq.append (Seq.filter filter merge) population |> Seq.take length |> Seq.toArray

        let random population generation =
            let length = Array.length population
            let merge = Array.append population generation
            chooseRandom merge length

        let randomBest ratio population generation =
            let length = Array.length population
            let size = int(double(length) / ratio)
            Array.append (chooseRandom population size) (chooseBest generation (length-size))

        let bestRandom ratio population generation =
            let length = Array.length population
            let size = int(double(length) / ratio)
            Array.append (chooseBest population size) (chooseRandom generation (length-size))
        
        let totalBestRandom ratio population generation = 
            let length = Array.length population
            let size = int(double(length) / ratio)
            let total = Array.append population generation
            Array.sortInPlaceBy (fun s -> -snd s) total
            let best = Array.take size total 
            let random = Array.skip size total 
            Array.append best random