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

///Functions that returns a sequence of couples of elements 
///selected from the population. The number of couples that are returned is unknown
module GeneticFramework.Generic.Selectors

let private rand = System.Random();;

///Returns random selected couples
let randSel population = 
    let size = Array.length population
    seq {for i in 0..size -> (fst(population.[rand.Next(size)]), fst(population.[rand.Next(size)]))}

///Returns random selected couples of different elements
let distSel population = 
    Seq.filter (fun (x,y) -> x <> y) (randSel population)

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

