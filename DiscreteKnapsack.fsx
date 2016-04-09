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

#load "Reproduction.fs" "Selectors.fs" "Distributors.fs" "GenericCrossers.fs" 
#load "Evolution.fs" "IntegerMutators.fs" "Utils.fs"
open GeneticFramework.Generic.Reproduction 
open GeneticFramework.Generic.Selectors
open GeneticFramework.Generic.Distributors
open GeneticFramework.Generic.Crossers
open GeneticFramework.Generic.Evolution
open GeneticFramawork.Integer.Mutators
open GeneticFramework.Generic.Utils
open GeneticFramework.Integer.Utils

(*This is an example of how use GeneticFramework to build an algorithm to solve a problem.
I build various custom functions, using only basic elements in the framework, to solve the Discrete Knapsack problem.*)
 
//problem variables
let objects = 12
let maxSel = 25 //max number of instance of the same object
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
let changes = 15 //number of iterations of the algorithms without which the solution changes

//Algorithms configuration:
//mutation configuration
let mutator = addModMut randDist maxSel
//reproduction configuration
(*the reproduction routines use the standard cross operation with a single random pivot,
the selection operation choose 2 random elements from the population.*)
let repr = stdReproduction  (singleCross randDist) randSel fitness mutator
let repr2 = stdReproduction  (singleCross randDist) randSel fitness2 mutator
(*to improve clarity fix some elements of the pipelines:*)
//initializes the population with random elements and fits
let init = randInit 0 fitness maxSel objects 
let init2 = randInit 0 fitness2 maxSel objects 
//a list of single element of only 0 
let zero = [(Array.zeroCreate 12: int[])]

let expander2 = limitExpander repr2 20 //expand the population until 20 elements using repr2
let expander = limitExpander repr 20 //expand the population until 20 elements using repr
let res1 = restrictor 1: (int[] * int)[]->(int[] * int)[] //restrict the population at 1 element
let res5 = restrictor 5: (int[] * int)[]->(int[] * int)[] //restrict the population at 5 elements

//algorithms:
(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the first Heuristic, and restrict the population at 1 element*)
let solution1 = res1 <| (eliteEvolution repr changes <| init elements);;
(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm with using the first Heuristic,
and restrict the population at 1 element*)
let solution2 = res1 <| (eliteEvolution  repr changes <| (expander <| (elements2Population fitness <| zero)));;

(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the second Heuristic, restrict the population at 1 element, and reevaluate this to compare with other solutions*)
let solution3 = refit fitness <| (res1 <| (eliteEvolution repr changes <| init elements));;

(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm with using the second Heuristic,
restrict the population at 1 element, and reevaluate this to compare with other solutions*)
let solution4 = refit fitness <| (res1 <| (eliteEvolution  repr changes <| (expander <| (elements2Population fitness <| zero))));;

(*This algorithm start from a random generated element, applay the standard genetic algorithm
with using the second Heuristic. The second heuristc allow  not accepttables instances,
therefore the population is reevaluated with the first heuristic, restricted at 5 elements.
Therefore the algorithms expand the population until populationSize and apply applay the standard genetic algorithm
with using the first Heuristic to obtain the bests acceptables instances from not acceptable population.
Finally restrict the population at 1 element*) 
let solution5 = res1 <| (eliteEvolution repr changes <| (res5 <| (refit fitness <| (eliteEvolution repr2 changes <| init2 elements))));;


(*This algorithm start from a list of a single array initialized to 0, expand the population
until populationSize, applay the standard genetic algorithm with using the first Heuristic. 
The second heuristc allow  not accepttables instances, therefore the population is reevaluated with the first heuristic, 
restricted at 5 elements.
Therefore the algorithms expand the population until populationSize and apply applay the standard genetic algorithm
with using the first Heuristic to obtain the best acceptables instances from not acceptable population.
Finally restrict the population at 1 element*)
let solution6 = res1 <| (eliteEvolution repr changes <| (res5 <| (refit fitness <| (eliteEvolution repr2 changes <| (expander2 <| (elements2Population fitness2 <| zero))))));;