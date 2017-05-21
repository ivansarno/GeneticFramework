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

module Reproduction =

        let private rand = System.Random()
        let private mutation mutator (elm1, elm2) = 
            mutator elm1
            mutator elm2
            (elm1, elm2)

        let private fit (fitness:'a -> int) (elm1, elm2) = ((elm1, fitness elm1), (elm2, fitness elm2))

        ///Standard reproduction routine
        let stdReproduction crosser selector fitness mutator = fun population ->
            let couples = selector population 
            let subroutine = crosser >> mutation mutator >> fit fitness
            (Seq.map subroutine couples) |> Seq.collect (fun (x,y) -> seq {yield x; yield y})|> Seq.toArray
        
        let reproduction2 crosser selector fitness mutator ratio = fun population ->
            let couples = selector population 
            let subroutine = crosser >> fit fitness
            let generation = Seq.map subroutine couples |> Seq.collect (fun (x,y) -> seq {yield x; yield y})|> Seq.toArray
            let total = Array.length generation
            let length = int(double(total) * ratio)
            let indexs = Seq.initInfinite (fun _ -> rand.Next total)|> Seq.distinct |> Seq.take length
            for i in indexs do generation.[i] <- mutator generation.[i]
            generation



