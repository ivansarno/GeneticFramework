import functools
from genetic.bit import initializers, mutators
from genetic.generic import distributors, selectors, algorithms

__author__ = 'ivansarno'
__version__ = 'beta'
__doc__ = """Example of algorithm for binary knapsack problem"""


def fitness(element, values, weights, max_weight):
    total_value = 0
    total_weight = 0
    for i, e in enumerate(element):
        if e:
            total_value += values[i]
            total_weight += weights[i]
    if total_weight <= max_weight:
        return total_value
    else:
        return 0


def bkp(change, elements, length, values, weights, max_weight):
    population = initializers.rand_init(elements, length)
    f = functools.partial(fitness, values=values, weights=weights, max_weight=max_weight)
    m = mutators.repl_mut
    d = distributors.random_dist
    s = selectors.random_sel
    population = algorithms.standard(population, change, s, d, d, f, m, elements, elements)
    for i in range(5):
        print(population[i][0].bin, population[i][1])
