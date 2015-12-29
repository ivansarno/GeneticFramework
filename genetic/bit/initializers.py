import random
from typing import List, Tuple
from bitstring import BitArray

from genetic.generic.initializers import elements2population

__author__ = 'ivansarno'
__version__ = 'V.2'
__doc__ = """functions to produces a random initial population"""

###
# type definition
Population = List[Tuple[object, int]]
###


def rand_init(elements: int, length: int, fitness) -> Population:
    """Return a population composed by random elements.

    :param elements: number of elements
    :param length: number of bit of one element
    :return: random population
    """
    population = []
    for i in range(elements):
        r = random.getrandbits(length)
        ba = BitArray(length=length)
        ba.uint = r
        population.append(ba)
    return elements2population(population, fitness)
