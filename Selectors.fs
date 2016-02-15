///Functions that returns a sequence of couples of elements 
///selected from the population. The number of couples that are returned is unknown
module GeneticFramework.Generic.Selectors

let private rand = System.Random();;

///Returns random selected couples
let randSel population = 
    let size = Array.length population
    seq {for i in 0..size -> (fst(population.[rand.Next(size)]), fst(population.[rand.Next(size)]))}

///Returns random selected couples of different elements
let distSel population = 
    Seq.filter (fun (x,y) -> x <> y) (randSel population)

///Returns couples composed by elements in opposite positions
let revSel population =
    let length = Array.length population - 1
    //let couples = (length / 2) - 1
    //seq { for i in 0..couples -> (fst(population.[i]), fst(population.[length-i-1])) }
    let first = seq { for i in 0..length -> fst(population.[i]) }
    let second = seq { for i in length.. -1..0 -> fst(population.[i]) }
    Seq.zip first second 

///Returns couples composed by contiguous elements
let contSel population = 
    let size = (Array.length population) - 1
    seq { for i in 1..size -> (fst(population.[i-1]), fst(population.[i])) }


///Returns couples composed by dinstinct contiguous elements choosed 
let distContSel population = 
    let size = (Array.length population) - 1
    seq { for i in 1..2..size -> (fst(population.[i-1]), fst(population.[i])) }

    
