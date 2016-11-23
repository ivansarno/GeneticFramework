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


#load "Selectors.fs" "Crossers.fs" "Mutator.fs"
#load "Evolution.fs" "BitString.fs" "Utils.fs"
open GeneticFramework.Generic.Selectors
open GeneticFramework.Generic.Crossers
open GeneticFramework.Generic.Evolution
open GeneticFramework.Generic.Mutators
open GeneticFramawork.BitString.Mutators
open GeneticFramework.Generic.Utils
open GeneticFramawork.BitString.Utils
open GeneticFramawork.BitString.Crossers
open GeneticFramework.Generic.MetaOperator
open System.Collections

(*This example explains metaoperator usage approaching the Partition Problem. A solution to the problem is represented by a string of bits, 
the element i is included in the first partition if string[i] == true*)

//problem's variables
let elementsNumber = 10
let values = [|1;5; 3; 8; 5; 9; 26; 13; 30; 40|]

//simple heuristic
let fitness (element: BitArray) = 
    let mutable sum = 0
    for i in 0..elementsNumber-1 do 
        sum <- if element.Get(i) then sum + values.[i] else sum - values.[i]
    -abs sum

(*Selector configuration: we want a first selector that mates parents similar values, a second selector that mates parent with higth value 
with parent with low value and a thried selector that mates randomly, and we want to use the first selector with the 50% probability, 30% for the second 
an 20 % for the thrid*)

let selector = probOp [|0.5; 0.30; 0.20|] [|contSel; revSel; randSel|]

(*Mutator configuration: we wont a mutation ratio of 40% and apply from 1 to elementsNumber/4 mutation to each element, 
a mutation consist ofthe negation of a random bit*)

let mutator = optionalMutator (randMultipleMutator notMut (elementsNumber/4)) 0.4

(*Crosser configuration: we combine 2 crossers, one take first 5 genes from first parent and the others from second parent, 
the other take odds genes from first parent and evens from second. The 2 crossers are selected randomly*)

let mask1 = BitArray([|true; true; true; true; true; false; false; false; false; false|])
let mask2 = BitArray([|true; false; true; false; true; false; true; false; true; false|])
let crosser = randOp [|maskCross mask1; maskCross mask2|]

(*Algorithm configuration: the initial population is conposed by strings of only zeros or ones. The algorithm performes evolution until 
for 10 consecutive iterations the sum of values of the element does not increase. At the end takes the best solution.*)
let populationSize = 20
let changes = 10
let population = Array.concat [|(zeroInit fitness elementsNumber (populationSize/2)); (oneInit fitness elementsNumber (populationSize/2))|]
let solution = sortRestrictor 1 <| grupEvolution crosser selector fitness mutator changes population