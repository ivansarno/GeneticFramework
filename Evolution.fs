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
//version V.1 beta

///Generic Evolution algorithms
module GeneticFramework.Generic.Evolution

let private merge population generation size =
    let next = Array.append population (Seq.toArray generation)
    if size > 0 then Array.sub next 0 size else next;;

let private sortMerge population generation size = 
     let next = Array.append population (Seq.toArray generation)
     Array.sortInPlaceBy (fun (x,y) -> -y) next
     if size > 0 then Array.sub next 0 size else next;;


///Standard evolution algorithm of the framework,
///For each iteration take the best elements of the population and the new generation(implements elitism)
///if best element not change for "changes" consecutive iterations the algorithms end.
///for optimize this algorithm use a lazy reproduction
let eliteEvolution reproduction changes population =
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
///for optimize this algorithm use a lazy reproduction
let grupEvolution reproduction changes population =
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
let countExpander reproduction count population = 
    let rec routine current count=
        if count = 0 then current
        else
            let next = merge population (reproduction current) -1
            routine next (count-1) in
    routine population count;;

///Expands the population until limit (new size)
let limitExpander reproduction limit population = 
    let rec routine current =
        if limit <= (Array.length current) then current
        else
            let next = merge population (reproduction current) -1
            routine next in
    routine population;;
        
///Expands the population of a factor
let percentExpander reproduction factor population =
    let limit = int(factor * float(Array.length population))           
    limitExpander reproduction limit population;;      
                

        