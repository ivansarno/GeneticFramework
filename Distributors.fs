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

///Functions to select a random index of a vector
module GeneticFramework.Generic.Distributors

let private rand = System.Random();;

///Return a random index
let randDist length =
    rand.Next(length);;

///Return a random index in the centre of the vector when
///breadth is the size of the central area
let centreDist breadth length =
    let index =  rand.Next(breadth + 1)
    let offset = length / 2 - breadth / 2
    index + offset;;


///Return a random index in one of the edges of the vector when
///breadth is the size of one edge area
let edgeDist breadth length =
    let index = rand.Next(breadth*2)
    if index <= breadth then index else length - index + breadth;;

///Return a random index in a range
let rangeDist first last ignore =
    rand.Next(first, last);;

///Returns a random index between start and start + breadth, where start and breadth are percentages of length.
let dynamicRangeDist start breadth length =
    let first = int(float(length) * start)
    let last = int(float(length) * breadth) + first
    rand.Next(first, last);;

///Return a index based on a probability destribution
///the fun probability take the size of the vector and return an array that
///represents a probability destribution.
let probabilisticDist probability length = 
    let choice = rand.NextDouble()
    let dist: float[] = probability length
    let mutable cumulative = dist.[0]
    let mutable i = 0
    while choice > cumulative do
        i <- i+1
        cumulative <- cumulative + dist.[i]
    i;; 
