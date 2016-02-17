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
        
        ///Srandard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population for count iterations.
        let stdCountExpander fitness mutator count population =
            let reproduction = difRandRepr fitness mutator
            countExpander reproduction count population
    
        ///Srandard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population until limit (new size).
        let stdLimitExpander fitness mutator limit population =
            let reproduction = difRandRepr fitness mutator
            limitExpander reproduction limit population

        ///Srandard algorithm for expansion of population with random selection, single random pivot for crossover.
        ///Expands the population of a factor.
        let stdPercent fitness mutator factor population =
                let reproduction = difRandRepr fitness mutator
                percentExpander reproduction factor population