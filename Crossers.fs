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

///Crossover operators
module GeneticFramework.Generic.Crossers

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
let private randomSwapCross swaps (parent1, parent2) =
    let son1 = Array.copy parent1
    let son2 = Array.copy parent1
    for _ in 0..swaps do
        let index = rand.Next (Array.length parent1)
        let temp = son1.[index]
        son1.[index] <- son2.[index]
        son2.[index] <- temp
    (son1, son2)

///Like randomSwapCross but number of swaps is random between 1 and maxSwaps.
let private dynamicSwapCross maxSwaps (parent1, parent2) = 
    let swaps = rand.Next(1, maxSwaps)
    randomSwapCross swaps (parent1, parent2)



///Swaps 2 aligned elements if a random number == 0 % period 
let private periodicSwapCross period (parent1, parent2) = 
   Array.unzip [| for (x,y) in Array.zip parent1 parent2 -> if rand.Next() % period = 0 then (y,x) else (x,y) |]
 
///Like periodicSwapCross but period is random between 1 and maxPeriod.  
let private randomPeriodicSwapCross maxPeriod (parent1, parent2) = periodicSwapCross (rand.Next(maxPeriod)) (parent1, parent2)

///Divides the parents in a central area and the edges, swaps this areas
///breadth is the percentage of the central area
let private centreCross breadth (parent1, parent2) =
    let pivot1 = int(float(Array.length parent1) * breadth / 2.0)
    let pivot2 = Array.length parent2 - pivot1
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1)







      
    