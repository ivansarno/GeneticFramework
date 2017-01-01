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


#load "Selectors.fs" "Crossers.fs" "Mutators.fs"
#load "Evolution.fs" "Integer.fs" "Utils.fs"
open GeneticFramework.Generic.Selectors
open GeneticFramework.Generic.Crossers
open GeneticFramework.Generic.Evolution
open GeneticFramawork.Integer.Mutations
open GeneticFramework.Generic.Utils
open GeneticFramework.Generic.Mutators
open GeneticFramawork.Integer.Utils

(*This is an example of how use GeneticFramework to build an algorithm to solve a problem. This example explains configuration and pipelining.
I build various custom functions, using only basic elements in the framework, to solve (naively) the Discrete Knapsack problem.*)
 
//problem variables
let objects = 12
let maxSel = 10 //max number of instance of the same object
let maxWaigth = 200
let waigths = [|20; 30; 40; 25; 60; 22; 37; 12; 30; 15; 80; 42|]
let values = [|15; 13; 55; 20; 25; 22; 2; 13; 40; 17; 26; 33|]

(*Heuristics functions: the first fitness function favors all acceptables instances over not acceptables 
instances; the second allow not accepttables instances, but with a penality*)
let fitness (element: int[]) = 
            let mutable value = 0
            let mutable waigth = 0
            for i in 0..objects-1 do
                value <- value + (element.[i] * values.[i])
                waigth <- waigth + (waigths.[i] * element.[i])
            if waigth <= maxWaigth then value else maxWaigth - waigth;;

let fitness2 (element: int[]) = 
            let mutable value = 0
            let mutable waigth = 0
            for i in 0..objects-1 do
                value <- value + (element.[i] *values.[i])
                waigth <- waigth + (waigths.[i] * element.[i])
            if waigth <= maxWaigth then value else int(double(value) * (double(maxWaigth) / double(waigth)) / 10.0)

//algoriths variables
let elements = 20 // number of elements of the populations
let changes = 10 //number of iterations of the algorithms without which the solution changes

//Algorithms configuration:
//mutation configuration
let mutator = singleMutator (addModMut maxSel)

(*to improve clarity fix some elements of the pipelines:*)
//initializes the population with random elements and fits
let init = randInit 0 fitness (maxSel/4) objects 
let init2 = randInit 0 fitness2 (maxSel/3) objects 
//a list of single element of only 0 
let zero = [(Array.zeroCreate 12: int[])]

//evolution algorithms configuration
let expander2 = limitExpander singleCross randSel fitness2 mutator 20 //expand the population until 20 elements using fitness2
let expander = limitExpander singleCross randSel fitness mutator 20 //expand the population until 20 elements using fitness
let restrictTo1 = sortRestrictor 1: (int[] * int)[]->(int[] * int)[] //restrict the population at 1 element
let restrictTo5 = sortRestrictor 5: (int[] * int)[]->(int[] * int)[] //restrict the population at 5 elements
let evolution1 = eliteEvolution singleCross randSel fitness mutator changes 
let evolution2 = eliteEvolution singleCross randSel fitness2 mutator changes 

//algorithms:
(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the first Heuristic, and restrict the population at 1 element*)
let solution1 = restrictTo1 <| (evolution1 <| init elements);;

(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm using the first Heuristic,
and restrict the population at 1 element*)
let solution2 = restrictTo1 <| (evolution1 <| (expander <| (elements2Population fitness <| zero)));;

(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the second Heuristic, restrict the population at 1 element, and reevaluate this to compare with other solutions*)
let solution3 = refit fitness <| (restrictTo1 <| (evolution2 <| init2 elements));;

(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm with using the second Heuristic,
restrict the population at 1 element, and reevaluate this to compare with other solutions*)
let solution4 = refit fitness <| (restrictTo1 <| (evolution2 <| (expander2 <| (elements2Population fitness2 <| zero))));;

(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the second Heuristic. The second heuristc allow  not accepttables instances,
therefore the population is reevaluated with the first heuristic, restricted at 5 elements.
Therefore the algorithms expand the population until populationSize and apply the standard genetic algorithm
with using the first Heuristic to obtain the bests acceptables instances from not acceptable population.
Finally restrict the population at 1 element*) 
let solution5 = restrictTo1 <| (evolution1<| (restrictTo5 <| (refit fitness <| (evolution2 <| init2 elements))));;


(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm  using the second Heuristic. 
The second heuristc allow  not accepttables instances, therefore the population is reevaluated with the first heuristic, 
restricted at 5 elements.
Therefore the algorithms expand the population until populationSize and apply applay the standard genetic algorithm
with using the first Heuristic to obtain the best acceptables instances from not acceptable population.
Finally restrict the population at 1 element*)
let solution6 = restrictTo1 <| (evolution1 <| (restrictTo5 <| (refit fitness <| (evolution2 <| (expander2 <| (elements2Population fitness2 <| zero))))));;