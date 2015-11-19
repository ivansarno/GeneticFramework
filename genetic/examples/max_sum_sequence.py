from genetic.integer import algorithms, mutators, initializers
from genetic.generic import distributors, selectors

__author__ = 'ivansarno'
__version__ = 'beta'
__doc__ = """Example of algorithm how find a sequence with max value from random integer sequences"""


def fitness(e):
    sum = 0
    for i in e:
        sum = sum + i
    return sum


def mss(elements=10, length=5, max_value=25, max_init=25, change=7):
    p = initializers.rand_init(elements, length, max_init)
    m = mutators.add_mut
    d = distributors.random_dist
    s = selectors.random_sel
    p = algorithms.standard(p, change, s, d, d, fitness, m, elements, elements, max_value)
    print(p[0])



