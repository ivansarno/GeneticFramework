from genetic import selectors, integer
from genetic.generic import distributors
from genetic.integer import mutators
from genetic.integer.algorithms import standard


def fitness(e):
    sum = 0
    for i in e:
        sum = sum + i
    return sum


def test():
    a = [1,2,3,4]
    b = [5,6,7,8]
    c = [9,10,11,12]
    d = [13,14,15,16]
    e = [17,18,19,20]

    p = [a,b,c,d,e]
    mutator = integer.mutators.add_mut
    p = standard(p, 7, selectors.random_sel, distributors.random_dist, distributors.random_dist, fitness, mutator, 5, 5, 25)
    print(p)






