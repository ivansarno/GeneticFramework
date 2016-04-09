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

///Reproduction routines
module GeneticFramework.Generic.Reproduction

let private mutation mutator (elm1, elm2) = 
    mutator elm1
    mutator elm2
    (elm1, elm2);;

let private fit (fitness:'a -> int) (elm1, elm2) = ((elm1, fitness elm1), (elm2, fitness elm2))

///Standard reproduction routine
let stdReproduction crosser selector  fitness mutator = fun population ->
    let couples = selector population 
    let subroutine = crosser >> mutation mutator >> fit fitness
    (Seq.map subroutine couples) |> Seq.collect (fun (x,y) -> seq {yield x; yield y})

///Take a reproduction routine and return a routine that discards the elements 
///with lower value to the minimum value of the previous generation
let lazyReproduction reproduction = fun population ->
    let minimum: int = snd(Array.last population)
    Seq.filter (fun (x,y) -> y > minimum) (reproduction population) 
        
    