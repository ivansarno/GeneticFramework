﻿(*
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

///Basic mutation operator for Integers
module GeneticFramawork.Integer.Mutators

    let private rand = System.Random();;

    let addModMut maxValue element =
        let index = rand.Next(Array.length element)
        element.[index] <- (element.[index] + rand.Next(0, maxValue)) % maxValue;;

    let subModMut maxValue element =
        let index = rand.Next(Array.length element)
        element.[index] <- (element.[index] - rand.Next(0, maxValue)) % maxValue;;
         

    let mulModMut maxValue element =
        let index = rand.Next(Array.length element)
        element.[index] <- (element.[index] * rand.Next(0, maxValue)) % maxValue;;

    
    let addMut minValue maxValue element =
        let index = rand.Next(Array.length element)
        let num = (element.[index] + rand.Next(minValue, maxValue))
        element.[index] <- if num > maxValue then maxValue else num;;

    let subMut minValue maxValue element =
        let index = rand.Next(Array.length element)
        let num = (element.[index] - rand.Next(minValue, maxValue))
        element.[index] <- if num < minValue then minValue else num;;
         

    let mulMut minValue maxValue element =
        let index = rand.Next(Array.length element)
        let num = (element.[index] * rand.Next(minValue, maxValue))
        if num > maxValue then element.[index] <- maxValue
        elif num < minValue then element.[index] <- minValue
        else element.[index] <- num;;

    let divMut minValue maxValue element =
        let index = rand.Next(Array.length element)
        let num = (element.[index] / rand.Next(minValue, maxValue))
        if num > maxValue then element.[index] <- maxValue
        elif num < minValue then element.[index] <- minValue
        else element.[index] <- num;;

    
    let andMut minValue maxValue element =
        let index = rand.Next(Array.length element) 
        element.[index] <- (element.[index] &&& rand.Next(minValue, maxValue)) % maxValue;;

    let orMut minValue maxValue element =
        let index = rand.Next(Array.length element) 
        element.[index] <- (element.[index] ||| rand.Next(minValue, maxValue)) % maxValue;;

    let xorMut minValue maxValue element =
        let index = rand.Next(Array.length element) 
        element.[index] <- (element.[index] ^^^ rand.Next(minValue, maxValue)) % maxValue;;

    let negMut element =
        let index = rand.Next(Array.length element)
        element.[index] <- -element.[index];;

    let negModMut modulus element =
        let index = rand.Next(Array.length element) 
        element.[index] <- -element.[index] % modulus ;;

    let replMut minValue maxValue element =
        let index = rand.Next(Array.length element) 
        element.[index] <- rand.Next(minValue, maxValue) % maxValue;;
