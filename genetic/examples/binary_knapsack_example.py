import functools
from genetic.bit import initializers, mutators
from genetic.generic import builtin

__author__ = 'ivansarno'
__version__ = 'V.2'
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
        return -total_weight


def binary_knapsack(change, elements, length, values, weights, max_weight):
    """Binary Knapsack Problem.

    :param change: max number of iteration without max value changes
    :param elements: number of elements of population
    :param length: number of objects to knapsack
    :param values: vector of values of the objects to knapsack
    :param weights: vector of weights of the objects to knapsack
    :param max_weight: max weights of the knapsack
    """
    f = functools.partial(fitness, values=values, weights=weights, max_weight=max_weight)
    mutator = mutators.repl_mut
    population = initializers.rand_init(elements, length, f)
    population = builtin.random_standard(population, fitness, mutator, change)
    print(population[0][0].bin, population[0][1])
