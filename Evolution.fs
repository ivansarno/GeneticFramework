﻿(*
    GeneticFramework

    Copyright 2015 Ivan Sarno

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
//version V.0.1

///Generic Evolution algorithms
module GeneticFramework.Generic.Evolution


let merge population generation =
    Seq.toArray (Seq.append population generation)

let mergeRestrict size population generation =
    Seq.append population generation |> Seq.sortBy (fun x -> -snd x) |> Seq.take size |> Seq.toArray

    
let private mutation mutator (elm1, elm2) = 
    mutator elm1
    mutator elm2
    (elm1, elm2)

let private fit (fitness:'a -> int) (elm1, elm2) = ((elm1, fitness elm1), (elm2, fitness elm2))

///Standard reproduction routine
let private stdReproduction crosser selector fitness mutator = fun population ->
    let couples = selector population 
    let subroutine = crosser >> mutation mutator >> fit fitness
    (Seq.map subroutine couples) |> Seq.collect (fun (x,y) -> seq {yield x; yield y})

///Take a reproduction routine and return a routine that discards the elements 
///with lower value to the minimum value of the previous generation
let lazyReproduction reproduction = fun population ->
    let minimum: int = snd(Array.last population)
    Seq.filter (fun (x,y) -> y > minimum) (reproduction population) 


///Standard evolution algorithm of the framework,
///For each iteration take the best elements of the population and the new generation(implements elitism)
///if best element not change for "changes" consecutive iterations the algorithms end.
let eliteEvolution crosser selector fitness mutator changes population =
    let reproduction = lazyReproduction (stdReproduction crosser selector mutator fitness)
    let rec routine (current: ('a*int) []) attempts =
        if attempts = 0 then current
        else
            let threshold = Array.maxBy (fun x -> snd x) population
            let generation = mergeRestrict (Array.length population) population (reproduction current)
            let max = Array.maxBy (fun x -> snd x) generation
            if max > threshold then routine generation changes
            else routine generation (attempts-1) in
    routine (Array.sortBy (fun (x,y) -> -y) population) changes;;


///For each iteration take the best elements of the population and the new generation(implements elitism)
///if the sum of value of all elements of the population not change for "changes" consecutive iterations the algorithms end.
let grupEvolution crosser selector fitness mutator changes population =
    let reproduction = lazyReproduction (stdReproduction crosser selector mutator fitness)
    let rec routine current max attempts =
        if attempts = 0 then current
        else
            let generation = mergeRestrict (Array.length population) population (reproduction current)
            let sum = Array.sumBy (fun (x,y) -> y) current
            if sum > max then routine generation sum changes
            else routine generation max (attempts-1)
    let sum = Array.sumBy (fun (x,y) -> y) population
    routine population sum changes;;


///Expands the population for count iterations
let countExpander crosser selector fitness mutator count population = 
    let reproduction = stdReproduction crosser selector mutator fitness
    let rec routine current count=
        if count = 0 then current
        else
            let generation = merge population (reproduction current) 
            routine generation (count-1) in
    routine population count;;

///Expands the population until limit (new size)
let limitExpander crosser selector fitness mutator limit population = 
    let reproduction = stdReproduction crosser selector mutator fitness
    let rec routine current =
        if limit <= (Array.length current) then current
        else
            let generation = merge population (reproduction current) 
            routine generation in
    routine population;;
        
///Expands the population of a factor
let percentExpander crosser selector fitness mutator factor population =
    let limit = int(factor * float(Array.length population))           
    limitExpander crosser selector fitness mutator limit population;;


///Apply an evolution algorithm on various population produced by an initializer
///and return a population composed by the bests elements of the sub-populations
///elements is the number of elements of one sub-population
///grups is the number of sub-population.
///This algorthm partially avoid the crouding
let private iterativeEvolution evolution initializer elements grups =
    let first (x: 'T []) = x.[0] 
    [|for _ in 0..grups ->  first(evolution (initializer elements))|]
    