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

///Crossover operators
module GeneticFramework.Generic.Crossers

let private rand = System.Random();;

///Single point cross, distributor fun select a random pivot
let singleCross distributor ((parent1, parent2): ('a[]*'a[])) =
    let pivot = distributor((Array.length parent1))
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son1, son2);;

///Single point cross on elements of different size, 
///distributor fun selects a random pivot
let difSingleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let pivot = distributor size
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son2, son1);;

///Double point cross on elements of different size, 
///distributor fun selects a random pivot
let doubleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let x = distributor size
    let mutable y = distributor size
    while x=y do
        y <- distributor size
    let pivot1 = min x y
    let pivot2 = max x y
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1);;