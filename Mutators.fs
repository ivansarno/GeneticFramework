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

    ///Apply a mutation on a random gene
    let singleMutator mutation chromosoma = 
        let index = rand.Next (Array.length chromosoma) 
        chromosoma.[index] <- mutation chromosoma.[index]

    ///Apply a mutation on random genes
    let multiMutator mutation mutNumber chromosoma =
        for i in 1..mutNumber do
            let index = rand.Next (Array.length chromosoma) 
            chromosoma.[index] <- mutation chromosoma.[index]
    
    ///Like multiMutator with a random number of mutations
    let randMultiMutator mutation maxMut chromosoma =
        multiMutator mutation (rand.Next(1, maxMut)) chromosoma

    ///Apply a mutation to all genes 
    let allMutator mutation probability chromosoma =
        let length = Array.length chromosoma - 1
        for i in 0..length do
            chromosoma.[i] <- if rand.NextDouble() >= probability 
                                then mutation chromosoma.[i] else chromosoma.[i]

    ///Apply a random mutation from the mutations array to all genes 
    let randMutator mutations probability chromosoma =
        let length = Array.length chromosoma - 1
        let mutNumber = Array.length mutations
        for i in 0..length do
            let mut = rand.Next(mutNumber)
            chromosoma.[i] <- if rand.NextDouble() >= probability 
                                then mutations.[mut] chromosoma.[i] else chromosoma.[i]