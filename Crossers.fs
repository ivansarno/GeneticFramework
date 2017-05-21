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


///Crossover operators
module GeneticFramework.Generic.Crossers
open System.Collections

let private rand = System.Random();;

///Single point cross
let singleCross ((parent1, parent2): ('a[]*'a[])) =
    let pivot = rand.Next(Array.length parent1)
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son1, son2);;



///Double point cross  
let doubleCross (parent1: 'a [], parent2: 'a []) =
    let length = Array.length parent1 
    let x = rand.Next length
    let mutable y = rand.Next length
    while x=y do
        y <- rand.Next length
    let pivot1 = min x y
    let pivot2 = max x y
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1)



///Swap the elements at a random index
///swaps is the number of the swaps  
let private swapCross swaps (parent1, parent2) =
    let son1 = Array.copy parent1
    let son2 = Array.copy parent1
    for _ in 1..swaps do
        let index = rand.Next (Array.length parent1)
        let temp = son1.[index]
        son1.[index] <- son2.[index]
        son2.[index] <- temp
    (son1, son2)

///Like swapCross but number of swaps is random between 1 and maxSwaps.
let private randSwapCross maxSwaps (parent1, parent2) = 
    let swaps = rand.Next(maxSwaps)
    swapCross swaps (parent1, parent2)


///Divides the parents in a central area and the edges, swaps this areas
///breadth is the percentage of the central area
let centreCross breadth (parent1, parent2) =
    let pivot1 = int(float(Array.length parent1) * breadth / 2.0)
    let pivot2 = Array.length parent2 - pivot1
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1)


let maskCross (mask: BitArray) (parent1: 'a[], parent2: 'a[]) =
    let length = Array.length parent1
    let son1 = [|for i in 0..length-1  -> if mask.Get(i) then parent1.[i] else parent2.[i]|]
    let son2 = [|for i in 0..length-1  -> if mask.Get(i) then parent2.[i] else parent1.[i]|]
    (son2, son1)

///version of maskCross with random mask
let randMaskCross length (parent1, parent2) =
    let temp = [|for i in 1..length -> rand.Next(2) = 0|]
    let mask = BitArray(temp)
    maskCross mask (parent1, parent2)

///Return the copy of the parents
let nullCross (parent1, parent2) = (Array.copy parent1, Array.copy parent2)


let uniformCross (parent1, parent2: 'a[]) =
    let length = Array.length parent1 - 1
    [|for i in 0..length -> if rand.Next(2) = 0 then (parent1.[i], parent2.[i]) else (parent2.[i], parent1.[i])|]
    |> Array.unzip 


let segmentedCross probability (parent1, parent2) =
    let rec routine temp index (p1: 'a[], p2) =
        if index = -1 then temp else
            let parents = if rand.NextDouble() >= probability then (p2,p1) else (p1,p2)
            routine ((p1.[index], p2.[index])::temp) (index-1) parents
    routine [] (Array.length parent1 - 1) (parent1, parent2)
    |> List.toArray |> Array.unzip

