(*
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

let private merge population generation size =
    let next = Array.append population (Seq.toArray generation)
    if size > 0 then Array.sub next 0 size else next;;

let private sortMerge population generation size = 
     let next = Array.append population (Seq.toArray generation)
     Array.sortInPlaceBy (fun (x,y) -> -y) next
     if size > 0 then Array.sub next 0 size else next;;
     
let private protoFM population generation size =
    let _,min = Array.last population 
    let bests = Seq.filter (fun (_,y) -> y > min) generation
    sortMerge population bests size
    
let private mutation mutator (elm1, elm2) = 
    mutator elm1
    mutator elm2
    (elm1, elm2);;

let private fit (fitness:'a -> int) (elm1, elm2) = ((elm1, fitness elm1), (elm2, fitness elm2))

///Standard reproduction routine
let private stdreproduction crosser selector  fitness mutator = fun population ->
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
    let reproduction = lazyReproduction (stdreproduction crosser selector mutator fitness)
    let rec routine (current: ('a*int) []) attempts =
        if attempts = 0 then current
        else
            let max = snd(current.[0])
            let next = sortMerge population (reproduction current) (Array.length population)
            if snd(next.[0]) > max then routine next changes
            else routine next (attempts-1) in
    routine (Array.sortBy (fun (x,y) -> -y) population) changes;;
        

///For each iteration take the best elements of the population and the new generation(implements elitism)
///if the sum of value of all elements of the population not change for "changes" consecutive iterations the algorithms end.
let grupEvolution crosser selector fitness mutator changes population =
    let reproduction = lazyReproduction (stdreproduction crosser selector mutator fitness)
    let rec routine current max attempts =
        if attempts = 0 then current
        else
            let next = sortMerge population (reproduction current) (Array.length population)
            let sum = Array.sumBy (fun (x,y) -> y) current
            if sum > max then routine next sum changes
            else routine next max (attempts-1) in
    let sum = Array.sumBy (fun (x,y) -> y) population
    routine population sum changes;;


///Expands the population for count iterations
let countExpander crosser selector fitness mutator count population = 
    let reproduction = stdreproduction crosser selector mutator fitness
    let rec routine current count=
        if count = 0 then current
        else
            let next = merge population (reproduction current) -1
            routine next (count-1) in
    routine population count;;

///Expands the population until limit (new size)
let limitExpander crosser selector fitness mutator limit population = 
    let reproduction = stdreproduction crosser selector mutator fitness
    let rec routine current =
        if limit <= (Array.length current) then current
        else
            let next = merge population (reproduction current) -1
            routine next in
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
 
//-------------------Prototypes-----------------------------------------------------------------------    
let private proto2 reproduction split stop population =
    let rec routine population =
        let toNext, toRep: 'a seq * 'a seq = split population
        let generation = Array.ofSeq (Seq.append toNext (reproduction toRep))
        if stop generation then generation else routine generation
    routine population