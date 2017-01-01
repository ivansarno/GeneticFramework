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


///Basic mutation operator for Real
namespace GeneticFramawork.Real
    module Mutions =    

        let private rand = System.Random()

        let addMut minValue maxValue gene = max minValue (min (gene + (rand.NextDouble() * (maxValue - minValue) + minValue)) maxValue)
            
        let subMut minValue maxValue gene = max minValue (min (gene - (rand.NextDouble() * (maxValue - minValue) + minValue)) maxValue)
                    
        let mulMut minValue maxValue gene = max minValue (min (gene * (rand.NextDouble() * (maxValue - minValue) + minValue)) maxValue)
            

        let divMut minValue maxValue gene =
            let mutable num = (rand.NextDouble() * (maxValue - minValue) + minValue)
            num <- if num = 0.0 then num + 1.0 else num
            max minValue (min (gene / num) maxValue)

        let negMut (gene: double) = -gene

        let replMut minValue maxValue gene = rand.NextDouble() * (maxValue - minValue) + minValue 


    module Utils=
        let private rand = System.Random()

        ///Returns a population of random elements
        let randInit minValue (fitness:double[]->int) maxValue length elements =
            [|for i in 0..elements-1 -> 
                let e = [|for i in 0..length-1 -> (rand.NextDouble() * (maxValue - minValue) + minValue)|]
                in (e, fitness e)|]

        ///Returns a population of elements of only zero
        let zeroInit (fitness:double[]->int) length elements =
            let temp = Array.zeroCreate length
            let temp2 = (temp, fitness temp)
            [|for i in 0..elements-1 -> temp2|]
        
        ///Returns a population of elements of only one
        let oneInit (fitness:double[]->int) length elements =
            let temp = Array.create length 1.0
            let temp2 = (temp, fitness temp)
            [|for i in 0..elements-1 -> temp2|]