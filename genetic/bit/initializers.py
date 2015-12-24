import random
from typing import List
from bitstring import BitArray

__author__ = 'ivansarno'
__version__ = 'V.1'
__doc__ = """funtions to produces a random intial population"""


def rand_init(elements: int, length: int) -> List[BitArray]:
    """Return a population conposed by random elements.

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
    return population
