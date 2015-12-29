import random
from typing import List, Tuple

from genetic.generic.initializers import elements2population

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """functions to produces a random initial population"""

###
# type definition
Population = List[Tuple[object, int]]
###


def rand_init(elements: int, length: int, fitness, max_value: int, min_value: int = 0) -> Population:
    """ Return a population composed by random elements.

    :param elements: number of elements
    :param length: number of int for element
    :param max_value: max value of integers
    :param min_value: min value of integers
    :return: random population
    """
    population = []
    for i in range(elements):
        new = []
        for j in range(length):
            new.append(random.randint(min_value, max_value - 1))
        population.append(new)
    return elements2population(population, fitness)
