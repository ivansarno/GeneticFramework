///Generic mutation operators
module GeneticFramework.Generic.Mutators

    let private rand = System.Random()
    
    ///apply a basic mutator with a probability
    let optionalMutator mutator probability element =
          if rand.NextDouble() <= probability then
                mutator element

    ///apply a basic mutator at various genes of the element
    let multipleMutator mutator (element: 'a array) (number: int) =
        for _ in 0..number do
            mutator element
          
