import functools

from genetic import selectors, distributors, mutators
from genetic.main import complete


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
    mutator = functools.partial(mutators.add_mut, max_value=25)
    p = complete(p,7, selectors.random_sel, distributors.random_dist, distributors.random_dist, fitness, mutator, 5, 5)
    print(p)
