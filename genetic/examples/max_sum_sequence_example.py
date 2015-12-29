from genetic.integer import mutators, metamutators, initializers
from genetic.generic import builtin

__author__ = 'ivansarno'
__version__ = 'V.2'
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
    :param max_value: max value of an element
    :param max_init: max value of an initial element
    :param change: max number of iteration without max value changes
    :return:
    """
    population = initializers.rand_init(elements, length, fitness, max_init)
    mutator = metamutators.int2gen(mutators.add_mut, max_value=max_value)

    p = builtin.random_standard(population, fitness, mutator, change)
    print(p[0])
