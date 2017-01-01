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

///Basic mutation operator for BitString
namespace GeneticFramawork.BitString
    open System.Collections
    module Mutators =    

        let private rand = System.Random()

        let replMut (element: BitArray) =
            let temp = rand.Next()
            let index = temp % element.Count
            element.Set(index, rand.Next() % 2 = 1)

        let setZeroMut (element: BitArray) =
            let index = rand.Next(element.Count)
            element.Set(index, false)

        let setOneMut (element: BitArray) =
            let index = rand.Next(element.Count)
            element.Set(index, true)
        
        let notMut (element: BitArray) =
            let index = rand.Next(element.Count)
            element.Set(index, not (element.Get(index)))

        let orMaskMut (mask: BitArray) (element: BitArray) =
            element.Or mask |> ignore
        
        let andMaskMut (mask: BitArray) (element: BitArray) =
            element.And mask |> ignore
        
        let xorMaskMut (mask: BitArray) (element: BitArray) =
            element.Xor mask |> ignore

        let notAllMut (element: BitArray) =
            element.Not() |> ignore

    module Utils =

        ///Returns a population of elements of only zero
        let zeroInit (fitness: BitArray -> int) length elements =
            let temp1 = BitArray(length, false)
            let temp2 = (temp1, fitness temp1)
            [|for i in 0..length-1 -> temp2|]

        ///Returns a population of elements of only one
        let oneInit (fitness: BitArray -> int) length elements =
            let temp1 = BitArray(length, true)
            let temp2 = (temp1, fitness temp1)
            [|for i in 0..length-1 -> temp2|]

    ///Cross operators specific for bitstring
    module Crossers =     
        
        ///
        let maskCross (mask:BitArray) =
            let reverse = BitArray(mask).Not()
            let crossHelper (parent1, parent2) =
                let son1 = (BitArray(mask).And parent1).Or (BitArray(reverse).And parent2)
                let son2 = (BitArray(mask).And parent2).Or (BitArray(reverse).And parent1)
                (son1, son2)
            crossHelper
        
        ///first son = parent1 and parent2, second son = ! first son  
        let andCross (parent1: BitArray, parent2: BitArray) =
            let son = BitArray(parent1).And parent2
            (son, BitArray(son).Not())
        
        ///first son = parent1 xor parent2, second son = ! first son  
        let xorCross (parent1: BitArray, parent2: BitArray) =
            let son = BitArray(parent1).Xor parent2
            (son, BitArray(son).Not())

        ///first son = parent1 or parent2, second son = ! first son  
        let orCross (parent1: BitArray, parent2: BitArray) =
            let son = BitArray(parent1).Or parent2
            (son, BitArray(son).Not())
            
            