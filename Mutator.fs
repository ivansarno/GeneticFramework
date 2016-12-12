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

///Generic mutation operators
module GeneticFramework.Generic.Mutators

    let private rand = System.Random()
    
    ///apply a basic mutator with a probability
    let optionalMutator mutator probability element =
          if rand.NextDouble() <= probability then
                mutator element

    ///apply a basic mutator at various genes of the element
    let multipleMutator mutator (mutations: int) element  =
        for _ in 1..mutations do
            mutator element

    ///apply a basic mutator at various a random number of genes of the element
    let randMultipleMutator mutator  (maxMutations: int) element = 
        multipleMutator mutator (rand.Next(1, maxMutations)) element 
          
    ///Swaps 2 random elements s
    let swapper parent =
        let index1 = rand.Next (Array.length parent)
        let index2 = rand.Next (Array.length parent)
        let temp = parent.[index1]
        parent.[index1] <- parent.[index2]
        parent.[index2] <- temp

    ///Applay all mutators on the element in sequence
    let sequenceMutator mutators element =
        for m in mutators do m element