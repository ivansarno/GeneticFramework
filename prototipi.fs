

//____________________EVOLUTION_______________________________________________

let private proto2 reproduction split stop population =
    let rec routine population =
        let toNext, toRep: 'a seq * 'a seq = split population
        let generation = Array.ofSeq (Seq.append toNext (reproduction toRep))
        if stop generation then generation else routine generation
    routine population

let private protoFM population generation size =
    let _,min = Array.last population 
    let bests = Seq.filter (fun (_,y) -> y > min) generation
    sortMerge population bests size





    


        
