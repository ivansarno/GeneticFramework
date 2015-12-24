from genetic.integer import algorithms, mutators, initializers
from genetic.generic import distributors, selectors

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """Example of algorithm how find a sequence with max value from random integer sequences"""


def fitness(e):
    total = 0
    for i in e:
        total = total + i
    return total


def max_sum_sequence(elements=10, length=5, max_value=25, max_init=25, change=7):
    """find a sequence with max value

    :param elements: number of elements of population
    :param length: of the sequence
    :param max_value: max value of an alement
    :param max_init: max value of an initial alement
    :param change: max number of iteration without max value changes
    :return:
    """
    p = initializers.rand_init(elements, length, max_init)
    m = mutators.add_mut
    d = distributors.random_dist
    s = selectors.random_sel
    p = algorithms.standard(p, change, s, d, d, fitness, m, elements, elements, max_value)
    print(p[0])
