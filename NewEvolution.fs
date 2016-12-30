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
    module NewEvolution =

        let genericEvolution reproduction stop replace population =
            let rec routine population =
                let generation = replace population (reproduction population)
                if stop population generation then generation else routine generation
            routine population

        ///prototype
        let genericDecroudEvolution reproduction stop replace decroud population =
            let rec routine population =
                let generation = decroud population <|replace population (reproduction population)
                if stop population generation then generation else routine generation
            routine population

    module Reproduction =

        let private merge population generation =
            Seq.toArray (Seq.append population generation)

        let private mergeRestrict size population generation =
            Seq.append population generation |> Seq.sortBy (fun x -> -snd x) |> Seq.take size |> Seq.toArray

            
        let private mutation mutator (elm1, elm2) = 
            mutator elm1
            mutator elm2
            (elm1, elm2)

        let private fit (fitness:'a -> int) (elm1, elm2) = ((elm1, fitness elm1), (elm2, fitness elm2))

        ///Standard reproduction routine
        let stdReproduction crosser selector fitness mutator = fun population ->
            let couples = selector population 
            let subroutine = crosser >> mutation mutator >> fit fitness
            (Seq.map subroutine couples) |> Seq.collect (fun (x,y) -> seq {yield x; yield y})

        ///Take a reproduction routine and return a routine that discards the elements 
        ///with lower value to the minimum value of the previous generation
        let lazyReproduction reproduction = fun population ->
            let minimum: int = snd(Array.last population)
            Seq.filter (fun (x,y) -> y > minimum) (reproduction population) 

        let generalReproduction crosser selector coupler fitness mutator = fun population -> 
            let couples = coupler <| selector population 
            let subroutine = crosser >> mutation mutator >> fit fitness
            (Seq.map subroutine couples) |> Seq.collect (fun (x,y) -> seq {yield x; yield y})
        
    module Replace =

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

    module Stop =

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


 
