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


///Basic mutation operator for Integers
namespace GeneticFramawork.Integer
    module Mutations =    

        let private rand = System.Random()

        let addModMut maxValue gene = (gene + rand.Next(maxValue)) % maxValue

        let subModMut maxValue gene = (gene - rand.Next(maxValue)) % maxValue
         

        let mulModMut maxValue gene = (gene * rand.Next(maxValue)) % maxValue

        let divModMut maxValue gene = 
            let mutable num = rand.Next(maxValue)
            num <- if num = 0 then num + 1 else num
            (gene / num) % maxValue

    
        let addMut minValue maxValue gene = max minValue (min (gene + rand.Next(minValue, maxValue)) maxValue)
            
        let subMut minValue maxValue gene = max minValue (min (gene - rand.Next(minValue, maxValue)) maxValue)
                    
        let mulMut minValue maxValue gene = max minValue (min (gene * rand.Next(minValue, maxValue)) maxValue)
            

        let divMut minValue maxValue gene =
            let mutable num = rand.Next(minValue, maxValue)
            num <- if num = 0 then num + 1 else num
            max minValue (min (gene / num) maxValue)
    
        let andMut minValue maxValue gene = max minValue (min (gene &&& rand.Next(minValue, maxValue)) maxValue)

        let orMut minValue maxValue gene = max minValue (min (gene ||| rand.Next(minValue, maxValue)) maxValue)

        let xorMut minValue maxValue gene = max minValue (min (gene ^^^ rand.Next(minValue, maxValue)) maxValue)

        let negMut gene = -gene

        let negModMut modulus gene = -gene % modulus

        let replMut minValue maxValue gene = rand.Next(minValue, maxValue) 

    module Utils=
        let private rand = System.Random()

        ///Returns a population of random genes
        let randInit minValue (fitness:int[]->int) maxValue length genes =
            [|for i in 1..genes -> 
                let e = [|for i in 1..length -> rand.Next(minValue, maxValue)|]
                in (e, fitness e)|]
        
        ///Returns a population of genes of only zero
        let zeroInit (fitness:int[]->int) length genes =
            let temp = Array.zeroCreate length
            let temp2 = (temp, fitness temp)
            [|for i in 1..genes -> temp2|]
        
        ///Returns a population of genes of only one
        let oneInit (fitness:int[]->int) length genes =
            let temp = Array.create length 1
            let temp2 = (temp, fitness temp)
            [|for i in 1..genes -> temp2|]
