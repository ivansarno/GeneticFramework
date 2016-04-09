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

///This namespace contains preconfigured operators, algorithms and routines.
namespace GeneticFramework.BuiltIn
    ///Preconfigured Reproduction routines.
    module Reproduction=
        open GeneticFramework.Generic.Selectors
        open GeneticFramework.Generic.Crossers
        open GeneticFramework.Generic.Distributors
        open GeneticFramework.Generic.Reproduction
        
        ///Reproduction routine with single random pivot for crossover and random selection.
        let randRepr fitness mutator = stdReproduction (singleCross randDist) randSel  fitness mutator

        ///Reproduction routine with single random pivot for crossover and random selection for element of different size.
        let difRandRepr fitness mutator = stdReproduction (difSingleCross randDist) randSel  fitness mutator

        ///Lazy Reproduction routine with single random pivot for crossover and random selection.
        let lazyRandRepr fitness mutator = lazyReproduction (randRepr fitness mutator)

        ///Lazy Reproduction routine with single random pivot for crossover and random selection for element of different size.
        let lazyDifRandRepr fitness mutator = lazyReproduction (difRandRepr fitness mutator);;

    ///Preconfigured Evolution algorithms.
    module Evolution=
        open Reproduction
        open GeneticFramework.Generic.Evolution

        ///Evolution algorithm with random selection, single random pivot for crossover, lazy reproduction, and elitism.
        let randEvolution fitness mutator changes population = 
            let repr = lazyRandRepr fitness mutator
            eliteEvolution repr changes population

        ///Evolution algorithm with random selection, single random pivot for crossover, lazy reproduction, and elitism
        ///for elements wit different size.
        let randDifEvolution fitness mutator changes population = 
            let repr = lazyDifRandRepr fitness mutator
            eliteEvolution repr changes population
        
        ///Standard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population for count iterations.
        let stdCountExpander fitness mutator count population =
            let reproduction = difRandRepr fitness mutator
            countExpander reproduction count population
    
        ///Standard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population until limit (new size).
        let stdLimitExpander fitness mutator limit population =
            let reproduction = difRandRepr fitness mutator
            limitExpander reproduction limit population

        ///Standard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population of a factor.
        let stdPercent fitness mutator factor population =
                let reproduction = difRandRepr fitness mutator
                percentExpander reproduction factor population