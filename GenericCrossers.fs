///Crossover operators
module GeneticFramework.Generic.Crossers

let private rand = System.Random();;

///Single point cross, distributor fun select a random pivot
let singleCross distributor ((parent1, parent2): ('a[]*'a[])) =
    let pivot = distributor((Array.length parent1))
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son1, son2);;

///Single point cross on elements of different size, 
///distributor fun selects a random pivot
let difSingleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let pivot = distributor size
    let son1 = Array.append (parent1.[..pivot]) (parent2.[pivot+1..])
    let son2 = Array.append (parent2.[..pivot]) (parent1.[pivot+1..])
    (son2, son1);;

///Double point cross on elements of different size, 
///distributor fun selects a random pivot
let doubleCross distributor (parent1, parent2) =
    let size = min (Array.length parent1) (Array.length parent2)
    let mutable x,y = (distributor size, distributor size)
    while x=y do
        y <- distributor size
    let pivot1 = min x y
    let pivot2 = max x y
    let son1 = Array.append (Array.append (parent1.[..pivot1]) (parent2.[pivot1+1..pivot2])) (parent1.[pivot2+1..])
    let son2 = Array.append (Array.append (parent2.[..pivot1]) (parent1.[pivot1+1..pivot2])) (parent2.[pivot2+1..])
    (son2, son1);;