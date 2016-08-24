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
    let pivot = rand.Next((Array.length parent1))
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son1, son2);;

///Single point cross on elements of different size, 
let difSingleCross (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let pivot = rand.Next size
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son2, son1);;

///Double point cross on elements of different size, 
let doubleCross (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let x = rand.Next size
    let mutable y = rand.Next size
    while x=y do
        y <- rand.Next size
    let pivot1 = min x y
    let pivot2 = max x y
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1);;
    
//-------------------Prototypes-----------------------------------------------------------------------

//same length  
let private proto1 period (parent1, parent2) = 
   Array.unzip [| for (x,y) in Array.zip parent1 parent2 -> if rand.Next() % period = 0 then (y,x) else (x,y) |]
 
//same length  
let private proto2 (parent1, parent2) = proto1 (rand.Next(Array.length parent1)) (parent1, parent2)

//same length
let private proto3 breadth (parent1, parent2) =
    let pivot1 = int(float(Array.length parent1) * breadth / 2.0)
    let pivot2 = Array.length parent2 - pivot1
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1)


//same length   
let private proto4 swaps (parent1, parent2) =
    let son1 = Array.copy parent1
    let son2 = Array.copy parent1
    for _ in swaps do
        let index = rand.Next(Array.length parent1) 
        let temp = son1.[index]
        son1.[index] <- son2.[index]
        son2.[index] <- temp
    (son1, son2)

//same length 
let private proto5 step (parent1, parent2: 'a[]) =
    let f i = i % (2*step) < step 
    Array.unzip [|for i in 0..Array.length parent1-1 -> if f i then (parent1.[i], parent2.[i]) else (parent2.[i], parent1.[i])|]
    
        
    