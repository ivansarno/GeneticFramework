import random
from typing import List

__author__ = 'ivansarno'
__version__ = 'V.1.0'
__doc__ = """funtions to produces a random intial population"""


def rand_init(elements: int, length: int, max_value: int, min_value: int = 0) -> List[List[int]]:
    """ Return a population conposed by random elements.

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
    return population
