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

///Generic utility functions
namespace GeneticFramework.Generic
    module Utils=

        ///Return a population from a list of elements
        let elements2Population (fitness:'a->int) elements =
            [|for e in elements -> (e, fitness e)|]

        ///Revalues a population with a different fitness functions
        let refit (fitness:'a->int) elements = [|for (e,v) in elements -> (e, fitness e)|]

        ///Restricts the population at a new size
        let restrictor newSize population = Array.sub population 0 newSize

        ///Sort and restrict the population at a new size
        let sortRestrictor newSize population = 
            Array.sortInPlaceBy (fun (x,y) -> -y) population
            Array.sub population 0 newSize;;

///Utility functions for Integers
namespace GeneticFramework.Integer
    module Utils=
        let private rand = System.Random()

        ///Returns a population of random elements
        let randInit minValue (fitness:int[]->int) maxValue length elements =
            [|for i in 0..elements-1 -> 
                let e = [|for i in 0..length-1 -> rand.Next(minValue, maxValue+1)|]
                in (e, fitness e)|];;

///Functions that creates complex operator from basic operator like mutators, selectors, reproduction, distributors or crossers                        
namespace GeneticFramework.Generic
    module MetaOperator=

        let private rand = System.Random()

        ///Apply a random operator from an array of operators 
        let randOp operators argument = 
            let index = rand.Next(0, Array.length operators)
            operators.[index] argument

        ///Apply an operator choosed from an array basing on a probability distribution
        ///the fun probability take the size of the vector and return an array that
        ///represents a probability destribution
        let probOp probability operators argument =
            let choice = rand.NextDouble()
            let dist: float[] = probability (Array.length operators)
            let mutable cumulative = dist.[0]
            let mutable i = 0
            while choice > cumulative do
                i <- i+1
                cumulative <- cumulative + dist.[i]
            operators.[i-1] argument