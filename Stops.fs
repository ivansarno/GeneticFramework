(*
    GeneticFramework

    Copyright 2017 Ivan Sarno

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

///Generic utility functions
namespace GeneticFramework.Generic
    module Stops=

        let countStop iterations =
            let mutable counter = iterations
            fun stop (population: ('a*int)[]) (generation: ('a*int)[]) ->
                counter <- counter - 1
                counter = 0

        let maxStop iterations =
            let mutable counter = iterations
            fun stop (population: ('a*int)[]) (generation: ('a*int)[]) ->
                let max = snd (Array.maxBy snd population)
                let newMax = snd (Array.maxBy snd generation)
                if newMax > max then counter <- iterations else counter <- counter-1
                counter = 0 

        let sumStop iterations =
            let mutable counter = iterations
            fun stop (population: ('a*int)[]) (generation: ('a*int)[]) ->
                let sum = Array.sumBy snd population
                let newSum = Array.sumBy snd generation
                if newSum > sum then counter <- iterations else counter <- counter-1
                counter = 0 

        let meanStop iterations =
            let mutable counter = iterations
            fun stop (population: ('a*int)[]) (generation: ('a*int)[]) ->
                let mean = Array.averageBy (snd>>double) population
                let newMean = Array.averageBy (snd>>double) generation
                if newMean > mean then counter <- iterations else counter <- counter-1
                counter = 0 

        let andStopCombination stops (population: ('a*int)[]) (generation: ('a*int)[]) =
            Array.forall (fun s -> s population generation)

        let orStopCombination stops (population: ('a*int)[]) (generation: ('a*int)[]) =
            Array.exists (fun s -> s population generation)