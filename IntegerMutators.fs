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

///Basic mutation operator for Integers
module GeneticFramawork.Integer.Mutators

    let private rand = System.Random();;

    let addModMut distributor max_value element =
        let index = distributor(Array.length element)
        element.[index] <- (element.[index] + rand.Next(0, max_value)) % max_value;;

    let subModMut distributor max_value element =
        let index = distributor(Array.length element)
        element.[index] <- (element.[index] - rand.Next(0, max_value)) % max_value;;
         

    let mulModMut distributor max_value element =
        let index = distributor(Array.length element)
        element.[index] <- (element.[index] * rand.Next(0, max_value)) % max_value;;

    
    let addMut distributor min_value max_value element =
        let index = distributor(Array.length element)
        let num = (element.[index] + rand.Next(min_value, max_value))
        element.[index] <- if num > max_value then max_value else num;;

    let subMut distributor min_value max_value element =
        let index = distributor(Array.length element)
        let num = (element.[index] - rand.Next(min_value, max_value))
        element.[index] <- if num < min_value then min_value else num;;
         

    let mulMut distributor min_value max_value element =
        let index = distributor(Array.length element)
        let num = (element.[index] * rand.Next(min_value, max_value))
        if num > max_value then element.[index] <- max_value
        elif num < min_value then element.[index] <- min_value
        else element.[index] <- num;;

    let divMut distributor min_value max_value element =
        let index = distributor(Array.length element)
        let num = (element.[index] / rand.Next(min_value, max_value))
        if num > max_value then element.[index] <- max_value
        elif num < min_value then element.[index] <- min_value
        else element.[index] <- num;;

    
    let andMut distributor min_value max_value element =
        let index = distributor(Array.length element) 
        element.[index] <- (element.[index] &&& rand.Next(min_value, max_value)) % max_value;;

    let orMut distributor min_value max_value element =
        let index = distributor(Array.length element) 
        element.[index] <- (element.[index] ||| rand.Next(min_value, max_value)) % max_value;;

    let xorMut distributor min_value max_value element =
        let index = distributor(Array.length element) 
        element.[index] <- (element.[index] ^^^ rand.Next(min_value, max_value)) % max_value;;

    let negMut distributor element =
        let index = distributor(Array.length element)
        element.[index] <- -element.[index];;

    let negModMut distributor modulus element =
        let index = distributor(Array.length element) 
        element.[index] <- -element.[index] % modulus ;;

    let replMut distributor min_value max_value element =
        let index = distributor(Array.length element) 
        element.[index] <- rand.Next(min_value, max_value) % max_value;;
